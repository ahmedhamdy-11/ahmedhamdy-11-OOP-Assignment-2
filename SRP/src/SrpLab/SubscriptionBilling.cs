using SrpLab.Subscription;

namespace SrpLab;

/// <summary>
/// Subscription billing: coordinates proration, invoicing, dunning, and ledger export.
/// </summary>
public sealed class SubscriptionBilling
{
    private readonly SubscriptionProrationCalculator _prorationCalculator = new();
    private readonly InvoiceNumberGenerator _invoiceGenerator = new();
    private readonly DunningEmailComposer _dunningComposer = new();
    private readonly LedgerJournalFormatter _ledgerFormatter = new();

    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public decimal Prorate(DateOnly activeFrom) =>
        _prorationCalculator.Prorate(MonthlyPrice, PeriodStart, PeriodEnd, activeFrom);

    public string NextInvoiceNumber() => _invoiceGenerator.Next(PeriodStart);

    public void RegisterFailedPayment() => FailedPayments++;

    public string DunningEmail(string customerName, DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber();
        return _dunningComposer.Compose(customerName, asOf, amount, invoice, FailedPayments);
    }

    public string LedgerJournalLine(DateOnly activeFrom) =>
        _ledgerFormatter.Format(CustomerId, NextInvoiceNumber(), Prorate(activeFrom));
}
