namespace SrpLab.Subscription;

/// <summary>Accounting journal export format — changes with ERP integration.</summary>
public sealed class LedgerJournalFormatter
{
    public string Format(string customerId, string invoiceNumber, decimal amount) =>
        $"{customerId},{invoiceNumber},{amount:0.00},AR-SUB";
}
