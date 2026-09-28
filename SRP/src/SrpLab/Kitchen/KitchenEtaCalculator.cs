namespace SrpLab.Kitchen;

/// <summary>Kitchen operations ETA model — changes with staffing and workflow.</summary>
public sealed class KitchenEtaCalculator
{
    public int Estimate(IReadOnlyList<int> prepMinutes, int openStations, int allergenCount)
    {
        if (openStations <= 0) openStations = 1;
        var sequential = prepMinutes.Sum();
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (allergenCount > 0) parallel += 3;
        var longest = prepMinutes.Count == 0 ? 0 : prepMinutes.Max();
        return Math.Max(parallel, longest);
    }
}
