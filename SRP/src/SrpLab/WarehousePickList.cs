using SrpLab.Warehouse;

namespace SrpLab;

/// <summary>
/// Warehouse pick list: coordinates stock allocation, path planning, and export formats.
/// </summary>
public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();
    private readonly StockAllocator _stockAllocator = new();
    private readonly WarehousePathPlanner _pathPlanner = new();
    private readonly PickerScriptFormatter _scriptFormatter = new();
    private readonly WmsBatchXmlExporter _wmsExporter = new();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate() =>
        _stockAllocator.Allocate(_lines.Select(l => (l.Sku, l.QtyNeeded, l.QtyOnHand)));

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder() =>
        _pathPlanner.WalkingOrder(_lines);

    public string PickerScript()
    {
        var walkingOrder = WalkingOrder();
        var shortfalls = Allocate().Select(a =>
        {
            var need = _lines.First(l => l.Sku == a.Sku).QtyNeeded;
            return (a.Sku, a.Allocated, QtyNeeded: need);
        }).Where(x => x.Allocated < x.QtyNeeded);
        return _scriptFormatter.Format(walkingOrder, shortfalls);
    }

    public string WmsXmlBatch(string batchId) => _wmsExporter.Export(batchId, Allocate());
}
