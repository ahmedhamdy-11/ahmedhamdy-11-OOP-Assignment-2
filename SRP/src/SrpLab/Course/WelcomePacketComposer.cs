namespace SrpLab.Course;

/// <summary>Welcome packet markdown content — changes with academy marketing.</summary>
public sealed class WelcomePacketComposer
{
    public string Compose(string courseCode, string studentName, string status)
    {
        return $"# Welcome to {courseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{courseCode.ToLowerInvariant()}\n";
    }
}
