namespace SrpLab.Runner;

/// <summary>Runs all lab demos in order without owning any domain scenario logic.</summary>
public sealed class LabDemoRunner
{
    private readonly IReadOnlyList<ILabDemo> _demos;

    public LabDemoRunner(IEnumerable<ILabDemo> demos) => _demos = demos.ToList();

    public static LabDemoRunner CreateDefault() => new LabDemoRunner(demos: [
        new Demos.WardBoardDemo(),
        new Demos.CheckoutBasketDemo(),
        new Demos.SupportTicketDemo(),
        new Demos.LoanDeskDemo(),
        new Demos.CourseEnrollmentDeskDemo(),
        new Demos.KitchenTicketDemo(),
        new Demos.SubscriptionBillingDemo(),
        new Demos.WarehousePickListDemo(),
        new Demos.GradeBookDemo(),
        new Demos.AppointmentDeskDemo(),
    ]);

    public void RunAll()
    {
        Console.WriteLine("SrpLab — 10 intentional SRP violations (refactor me)");
        Console.WriteLine("=================================================");

        foreach (var demo in _demos)
            demo.Run();

        Console.WriteLine("Done. Now split responsibilities — without breaking behavior.");
    }
}
