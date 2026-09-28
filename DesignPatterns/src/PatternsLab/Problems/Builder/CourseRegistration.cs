namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    public CourseRegistration(
        string studentEmail,
        string courseCode,
        string accessMode,
        string? groupCode,
        string? discountCode,
        bool sendWhatsApp,
        bool sendEmailWelcome,
        string? mentorNote,
        DateOnly? preferredStart)
    {
        if (string.IsNullOrWhiteSpace(studentEmail)) throw new ArgumentException("email required");
        if (string.IsNullOrWhiteSpace(courseCode)) throw new ArgumentException("course required");

        if (accessMode == "LiveGroup" && string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode");
        if (accessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode");

        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
        GroupCode = groupCode;
        DiscountCode = discountCode;
        SendWhatsApp = sendWhatsApp;
        SendEmailWelcome = sendEmailWelcome;
        MentorNote = mentorNote;
        PreferredStart = preferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";
}

public class CourseRegistrationBuilder
{
    private string? _studentEmail;
    private string? _courseCode;
    private string? _accessMode;
    private string? _groupCode;
    private string? _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome;
    private string? _mentorNote;
    private DateOnly? _preferredStart;

    public CourseRegistrationBuilder WithStudentEmail(string studentEmail)
    {
        _studentEmail = studentEmail;
        return this;
    }

    public CourseRegistrationBuilder WithCourseCode(string courseCode)
    {
        _courseCode = courseCode;
        return this;
    }

    public CourseRegistrationBuilder WithAccessMode(string accessMode)
    {
        _accessMode = accessMode;
        return this;
    }

    public CourseRegistrationBuilder AsLiveGroup(string groupCode)
    {
        _accessMode = "LiveGroup";
        _groupCode = groupCode;
        return this;
    }

    public CourseRegistrationBuilder AsVideosOnly()
    {
        _accessMode = "VideosOnly";
        _groupCode = null;
        return this;
    }

    public CourseRegistrationBuilder WithGroupCode(string? groupCode)
    {
        _groupCode = groupCode;
        return this;
    }

    public CourseRegistrationBuilder WithDiscountCode(string? discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public CourseRegistrationBuilder WithWhatsApp(bool sendWhatsApp = true)
    {
        _sendWhatsApp = sendWhatsApp;
        return this;
    }

    public CourseRegistrationBuilder WithEmailWelcome(bool sendEmailWelcome = true)
    {
        _sendEmailWelcome = sendEmailWelcome;
        return this;
    }

    public CourseRegistrationBuilder WithMentorNote(string? mentorNote)
    {
        _mentorNote = mentorNote;
        return this;
    }

    public CourseRegistrationBuilder WithPreferredStart(DateOnly? preferredStart)
    {
        _preferredStart = preferredStart;
        return this;
    }

    public CourseRegistration Build()
    {
        if (string.IsNullOrWhiteSpace(_studentEmail))
            throw new InvalidOperationException("StudentEmail is required");
        if (string.IsNullOrWhiteSpace(_courseCode))
            throw new InvalidOperationException("CourseCode is required");
        if (string.IsNullOrWhiteSpace(_accessMode))
            throw new InvalidOperationException("AccessMode is required");

        return new CourseRegistration(
            _studentEmail,
            _courseCode,
            _accessMode,
            _groupCode,
            _discountCode,
            _sendWhatsApp,
            _sendEmailWelcome,
            _mentorNote,
            _preferredStart);
    }
}

public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudent()
    {
        return new CourseRegistrationBuilder()
            .WithStudentEmail("sara@mail.com")
            .WithCourseCode("SEF-101")
            .AsLiveGroup("G1")
            .WithDiscountCode("EARLY10")
            .WithWhatsApp()
            .WithEmailWelcome()
            .WithMentorNote("Needs evening slot")
            .WithPreferredStart(new DateOnly(2026, 10, 1))
            .Build();
    }

    public static CourseRegistration CreateVideosOnly()
    {
        return new CourseRegistrationBuilder()
            .WithStudentEmail("ali@mail.com")
            .WithCourseCode("SEF-101")
            .AsVideosOnly()
            .WithEmailWelcome()
            .Build();
    }
}
