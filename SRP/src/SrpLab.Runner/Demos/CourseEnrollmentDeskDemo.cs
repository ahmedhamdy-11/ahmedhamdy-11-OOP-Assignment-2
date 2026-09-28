using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class CourseEnrollmentDeskDemo : ILabDemo
{
    public void Run()
    {
        var course = new CourseEnrollmentDesk("SEF-101", capacity: 1, tuition: 3000m);
        Console.WriteLine(course.Register("a@mail.com"));
        Console.WriteLine(course.Register("b@mail.com"));
        Console.WriteLine(course.WelcomePacketMarkdown("b@mail.com", "Bea"));
    }
}
