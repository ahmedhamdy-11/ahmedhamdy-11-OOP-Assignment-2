namespace SrpLab.Grading;

/// <summary>Grade book CSV export schema — changes with reporting requirements.</summary>
public sealed class GradeBookCsvExporter
{
    public string Export(IEnumerable<(string StudentId, decimal Average, string Letter, bool HonorRoll)> rows)
    {
        var lines = new List<string> { "studentId,average,letter,honor" };
        foreach (var row in rows.OrderBy(r => r.StudentId))
            lines.Add($"{row.StudentId},{row.Average},{row.Letter},{(row.HonorRoll ? 1 : 0)}");
        return string.Join('\n', lines);
    }
}
