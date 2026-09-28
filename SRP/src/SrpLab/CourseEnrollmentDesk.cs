using SrpLab.Course;

namespace SrpLab;

/// <summary>
/// Course enrollment: coordinates registration, welcome content, and invoicing.
/// </summary>
public sealed class CourseEnrollmentDesk
{
    private readonly EnrollmentRegistry _registry = new();
    private readonly WelcomePacketComposer _welcomeComposer = new();
    private readonly TuitionInvoiceFormatter _invoiceFormatter = new();

    public int Capacity { get; }
    public decimal Tuition { get; }
    public string CourseCode { get; }

    public CourseEnrollmentDesk(string courseCode, int capacity, decimal tuition)
    {
        CourseCode = courseCode;
        Capacity = capacity;
        Tuition = tuition;
    }

    public string Register(string studentEmail)
    {
        if (string.IsNullOrWhiteSpace(studentEmail)) throw new ArgumentException("email");
        return _registry.Register(studentEmail.Trim(), Capacity);
    }

    public int WaitlistPosition(string studentEmail) => _registry.WaitlistPosition(studentEmail);

    public string WelcomePacketMarkdown(string studentEmail, string studentName)
    {
        var status = _registry.IsSeated(studentEmail)
            ? "confirmed seat"
            : $"waitlist #{WaitlistPosition(studentEmail)}";
        return _welcomeComposer.Compose(CourseCode, studentName, status);
    }

    public string TuitionInvoiceLine(string studentEmail) =>
        _invoiceFormatter.Format(CourseCode, _registry.IsSeated(studentEmail), Tuition);

    public void PromoteFromWaitlist(int seats) => _registry.PromoteFromWaitlist(seats, Capacity);
}
