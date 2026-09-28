using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class LoanDeskDemo : ILabDemo
{
    public void Run()
    {
        var loan = new LoanDesk(60_000m, 640, 4, hasCollateral: false);
        Console.WriteLine(loan.DecisionLetter("Omar"));
    }
}
