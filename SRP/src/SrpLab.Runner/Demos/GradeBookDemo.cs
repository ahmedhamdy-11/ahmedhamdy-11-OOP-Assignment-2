using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class GradeBookDemo : ILabDemo
{
    public void Run()
    {
        var grades = new GradeBook();
        grades.Record("s1", 92);
        grades.Record("s1", 88);
        Console.WriteLine(grades.TranscriptPlain("s1", "Ali"));
    }
}
