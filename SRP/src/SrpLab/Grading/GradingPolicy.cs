namespace SrpLab.Grading;

/// <summary>Academic letter bands and honor roll rules — changes with faculty policy.</summary>
public sealed class GradingPolicy
{
    public string Letter(decimal average)
    {
        if (average >= 90) return "A";
        if (average >= 80) return "B";
        if (average >= 70) return "C";
        if (average >= 60) return "D";
        return "F";
    }

    public bool MeetsHonorRoll(decimal average, string letter) =>
        average >= 85 && letter is "A" or "B";
}
