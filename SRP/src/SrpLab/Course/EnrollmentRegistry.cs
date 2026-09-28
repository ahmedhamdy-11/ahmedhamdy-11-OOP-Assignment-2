namespace SrpLab.Course;

/// <summary>Seat capacity and waitlist management — changes with enrollment policy.</summary>
public sealed class EnrollmentRegistry
{
    private readonly HashSet<string> _seated = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<string> _waitlist = new();

    public int SeatedCount => _seated.Count;
    public bool IsSeated(string email) => _seated.Contains(email);

    public string Register(string email, int capacity)
    {
        if (_seated.Contains(email) || _waitlist.Contains(email))
            return "ALREADY_REGISTERED";

        if (_seated.Count < capacity)
        {
            _seated.Add(email);
            return "SEATED";
        }

        _waitlist.Add(email);
        return $"WAITLIST:{_waitlist.Count}";
    }

    public int WaitlistPosition(string email)
    {
        var idx = _waitlist.FindIndex(x => x.Equals(email, StringComparison.OrdinalIgnoreCase));
        return idx < 0 ? -1 : idx + 1;
    }

    public void PromoteFromWaitlist(int seats, int capacity)
    {
        while (seats > 0 && _waitlist.Count > 0 && _seated.Count < capacity)
        {
            var next = _waitlist[0];
            _waitlist.RemoveAt(0);
            _seated.Add(next);
            seats--;
        }
    }
}
