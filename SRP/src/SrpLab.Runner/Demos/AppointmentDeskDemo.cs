using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class AppointmentDeskDemo : ILabDemo
{
    public void Run()
    {
        var appt = new AppointmentDesk(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
        var slot = appt.FindNextSlot(DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48)
            ?? throw new InvalidOperationException("no slot");
        appt.TryBook(slot);
        Console.WriteLine(appt.SmsReminder(slot, "0100"));
    }
}
