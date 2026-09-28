using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class CheckoutBasketDemo : ILabDemo
{
    public void Run()
    {
        var basket = new CheckoutBasket();
        basket.AddLine("SKU-1", 40m, 2);
        basket.ApplyCouponText("SAVE10");
        basket.EnableGiftWrap();
        Console.WriteLine($"basket total={basket.GrandTotal()} auth={basket.AuthorizePaymentStub("4242")}");
    }
}
