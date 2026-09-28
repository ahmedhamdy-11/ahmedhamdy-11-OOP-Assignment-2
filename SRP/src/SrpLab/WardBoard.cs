using SrpLab.Ward;

namespace SrpLab;

/// <summary>
/// Hospital ward board: coordinates bed assignment, acuity scoring, handoff notes, and paging.
/// </summary>
public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();
    private readonly PagerLog _pagerLog = new();
    private readonly AcuityScorer _acuityScorer = new();
    private readonly HandoffNoteFormatter _handoffNoteFormatter = new();
    private readonly WardCensusExporter _censusExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = _acuityScorer.Score(heartRate, spo2);

        if (_vitalsScore[bed] >= 8)
            _pagerLog.AddYellowCode(bed);
    }

    public int ScoreAcuity(int heartRate, int spo2) => _acuityScorer.Score(heartRate, spo2);

    public string BuildHandoffNote(int bed) =>
        _handoffNoteFormatter.Format(bed, _bedPatient, _vitalsScore);

    public IReadOnlyList<string> DrainPagerLog() => _pagerLog.Drain();

    public string ExportCensusCsv() => _censusExporter.Export(_bedPatient, _vitalsScore);
}
