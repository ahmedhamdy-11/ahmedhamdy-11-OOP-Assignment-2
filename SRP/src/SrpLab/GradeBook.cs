using SrpLab.Grading;

namespace SrpLab;

/// <summary>
/// Grade book: coordinates score storage, grading policy, and transcript export.
/// </summary>
public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);
    private readonly GradingPolicy _gradingPolicy = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();
    private readonly GradeBookCsvExporter _csvExporter = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }
        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list) || list.Count == 0) return 0m;
        return Math.Round(list.Average(), 2);
    }

    public string Letter(string studentId) => _gradingPolicy.Letter(Average(studentId));

    public bool MeetsHonorRoll(string studentId)
    {
        var avg = Average(studentId);
        return _gradingPolicy.MeetsHonorRoll(avg, _gradingPolicy.Letter(avg));
    }

    public string TranscriptPlain(string studentId, string fullName) =>
        _transcriptFormatter.Format(studentId, fullName, Average(studentId), Letter(studentId), MeetsHonorRoll(studentId));

    public string ExportCsv()
    {
        var rows = _scores.Keys.Select(id => (id, Average(id), Letter(id), MeetsHonorRoll(id)));
        return _csvExporter.Export(rows);
    }
}
