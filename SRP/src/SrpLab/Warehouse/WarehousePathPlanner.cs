namespace SrpLab.Warehouse;

/// <summary>Warehouse walking path ordering — changes with layout technology.</summary>
public sealed class WarehousePathPlanner
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder(
        IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        return lines
            .OrderBy(l => l.Aisle)
            .ThenBy(l => l.Bin)
            .Select(l => (l.Aisle, l.Bin, l.Sku, Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(x => x.Item4 > 0)
            .ToList();
    }
}
