namespace SrpLab.Ward;

/// <summary>Pager alert logging — changes with paging infrastructure.</summary>
public sealed class PagerLog
{
    private readonly List<string> _entries = new();

    public void AddYellowCode(int bed) =>
        _entries.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");

    public IReadOnlyList<string> Drain()
    {
        var copy = _entries.ToList();
        _entries.Clear();
        return copy;
    }
}
