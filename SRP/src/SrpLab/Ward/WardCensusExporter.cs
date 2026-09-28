namespace SrpLab.Ward;

/// <summary>Census CSV export — changes with reporting / persistence schema.</summary>
public sealed class WardCensusExporter
{
    public string Export(IReadOnlyDictionary<int, string> bedPatient, IReadOnlyDictionary<int, int> vitalsScore)
    {
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var bed in bedPatient.Keys.OrderBy(x => x))
            lines.Add($"{bed},{bedPatient[bed]},{vitalsScore[bed]}");
        return string.Join('\n', lines);
    }
}
