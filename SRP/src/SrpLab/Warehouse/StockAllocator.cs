namespace SrpLab.Warehouse;

/// <summary>Stock allocation and backorder policy — changes with inventory rules.</summary>
public sealed class StockAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(
        IEnumerable<(string Sku, int QtyNeeded, int QtyOnHand)> lines)
    {
        return lines
            .Select(line => (line.Sku, Math.Min(line.QtyNeeded, line.QtyOnHand)))
            .ToList();
    }
}
