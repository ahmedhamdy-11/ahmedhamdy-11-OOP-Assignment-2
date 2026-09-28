using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class WardBoardDemo : ILabDemo
{
    public void Run()
    {
        var ward = new WardBoard();
        ward.AssignBed(1, "p-88", heartRate: 130, spo2: 89);
        Console.WriteLine(ward.BuildHandoffNote(1));
        Console.WriteLine(string.Join(" | ", ward.DrainPagerLog()));
    }
}
