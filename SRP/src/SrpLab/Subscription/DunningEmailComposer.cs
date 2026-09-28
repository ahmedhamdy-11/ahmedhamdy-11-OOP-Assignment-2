namespace SrpLab.Subscription;

/// <summary>Collections dunning email copy — changes with legal/comms team.</summary>
public sealed class DunningEmailComposer
{
    public string Compose(string customerName, DateOnly asOf, decimal balance, string invoiceNumber, int failedPayments)
    {
        var severity = failedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };
        return $"Subject: {severity} {invoiceNumber}\nHi {customerName},\nBalance {balance:C} as of {asOf:o} ({failedPayments} failures).\n";
    }
}
