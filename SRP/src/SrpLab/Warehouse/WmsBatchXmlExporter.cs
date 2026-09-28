namespace SrpLab.Warehouse;

/// <summary>WMS integration XML batch format — changes with warehouse system contract.</summary>
public sealed class WmsBatchXmlExporter
{
    public string Export(string batchId, IEnumerable<(string Sku, int Allocated)> allocations)
    {
        var parts = allocations.Select(a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");
        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}
