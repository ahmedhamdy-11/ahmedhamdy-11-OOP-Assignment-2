namespace SrpLab.Kitchen;

/// <summary>Thermal printer ticket layout — changes with hardware vendor.</summary>
public sealed class ThermalTicketRenderer
{
    private const int Width = 32;

    public string Render(
        int orderNumber,
        IEnumerable<(string Item, int PrepMinutes)> items,
        int etaMinutes,
        IReadOnlyList<string> allergens)
    {
        var line = new string('=', Width);
        var body = string.Join('\n', items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join(",", allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {etaMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
