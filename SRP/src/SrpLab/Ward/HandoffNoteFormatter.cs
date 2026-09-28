namespace SrpLab.Ward;

/// <summary>Nurse handoff narrative formatting — changes with documentation standards.</summary>
public sealed class HandoffNoteFormatter
{
    public string Format(int bed, IReadOnlyDictionary<int, string> bedPatient, IReadOnlyDictionary<int, int> vitalsScore)
    {
        if (!bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";

        var acuity = vitalsScore[bed];
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}
