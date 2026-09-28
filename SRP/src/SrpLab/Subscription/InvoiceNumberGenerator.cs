namespace SrpLab.Subscription;

/// <summary>Invoice numbering scheme — changes with fiscal/ops requirements.</summary>
public sealed class InvoiceNumberGenerator
{
    private static int _invoiceSeq = 1000;

    public string Next(DateOnly periodStart)
    {
        var n = ++_invoiceSeq;
        return $"INV-{periodStart:yyyyMM}-{n:D5}";
    }
}
