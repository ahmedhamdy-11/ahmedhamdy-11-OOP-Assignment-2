using SrpLab.Checkout;

namespace SrpLab;

/// <summary>
/// Online checkout basket: coordinates line items, pricing, coupons, gift wrap, and payment.
/// </summary>
public sealed class CheckoutBasket
{
    private const decimal GiftWrapFee = 4.99m;

    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private readonly CouponDiscountCalculator _couponCalculator = new();
    private readonly GiftMessageCardFormatter _giftMessageFormatter = new();
    private readonly PaymentAuthorizer _paymentAuthorizer = new();
    private string? _couponRaw;
    private bool _giftWrap;

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;

    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);

    public decimal DiscountAmount() => _couponCalculator.Calculate(_couponRaw, SubTotal());

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();
        if (_giftWrap) total += GiftWrapFee;
        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName) =>
        _giftMessageFormatter.Format(fromName, _lines.Select(l => l.Sku), GrandTotal());

    public string AuthorizePaymentStub(string cardLast4) =>
        _paymentAuthorizer.Authorize(GrandTotal(), cardLast4, _lines.Count);
}
