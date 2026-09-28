using SrpLab.Kitchen;

namespace SrpLab;

/// <summary>
/// Kitchen ticket: coordinates allergen detection, ETA estimation, and ticket rendering.
/// </summary>
public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();
    private readonly AllergenDetector _allergenDetector = new();
    private readonly KitchenEtaCalculator _etaCalculator = new();
    private readonly ThermalTicketRenderer _ticketRenderer = new();

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((item, ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens() =>
        _allergenDetector.Detect(_items.Select(i => i.Ingredients));

    public int EstimatedReadyMinutes(int openStations) =>
        _etaCalculator.Estimate(
            _items.Select(i => i.PrepMinutes).ToList(),
            openStations,
            DetectAllergens().Count);

    public string RenderThermalTicket(int orderNumber) =>
        _ticketRenderer.Render(
            orderNumber,
            _items.Select(i => (i.Item, i.PrepMinutes)),
            EstimatedReadyMinutes(2),
            DetectAllergens());

    public string ExpoLaneHint()
    {
        var allergens = DetectAllergens();
        return allergens.Count > 0 ? "LANE-ALLERGY" : EstimatedReadyMinutes(2) > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}
