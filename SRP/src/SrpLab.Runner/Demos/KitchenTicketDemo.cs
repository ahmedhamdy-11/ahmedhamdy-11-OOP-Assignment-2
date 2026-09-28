using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class KitchenTicketDemo : ILabDemo
{
    public void Run()
    {
        var kitchen = new KitchenTicket();
        kitchen.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
        Console.WriteLine(kitchen.RenderThermalTicket(42));
    }
}
