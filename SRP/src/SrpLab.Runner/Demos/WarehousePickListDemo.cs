using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class WarehousePickListDemo : ILabDemo
{
    public void Run()
    {
        var pick = new WarehousePickList();
        pick.AddNeed("BOLT", "A", 3, 10, 7);
        pick.AddNeed("NUT", "B", 1, 5, 5);
        Console.WriteLine(pick.PickerScript());
    }
}
