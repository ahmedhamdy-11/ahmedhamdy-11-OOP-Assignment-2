namespace SrpLab.Checkout;

/// <summary>Gift message customer-facing copy — changes with marketing.</summary>
public sealed class GiftMessageCardFormatter
{
    public string Format(string fromName, IEnumerable<string> skus, decimal grandTotal)
    {
        var items = string.Join(", ", skus);
        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {grandTotal:C}\n";
    }
}
