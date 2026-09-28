namespace SrpLab.Appointment;

/// <summary>SMS reminder message copy — changes with messaging channel team.</summary>
public sealed class SmsReminderComposer
{
    public string Compose(DateTimeOffset slot, string clinicPhone) =>
        $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
}
