using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class SubscriptionBillingDemo : ILabDemo
{
    public void Run()
    {
        var sub = new SubscriptionBilling("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
        sub.RegisterFailedPayment();
        Console.WriteLine(sub.DunningEmail("Sara", new DateOnly(2026, 9, 20)));
    }
}
