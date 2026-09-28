namespace SrpLab.Grading;

/// <summary>Registrar transcript document format — changes with document standards.</summary>
public sealed class TranscriptFormatter
{
    public string Format(string studentId, string fullName, decimal average, string letter, bool honorRoll) =>
        $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\nHonor: {honorRoll}\n";
}
