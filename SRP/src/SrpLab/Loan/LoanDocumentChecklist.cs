namespace SrpLab.Loan;

/// <summary>Compliance document requirements — changes with regulation.</summary>
public sealed class LoanDocumentChecklist
{
    public IReadOnlyList<string> Required(
        decimal requestedAmount,
        int employmentMonths,
        bool hasCollateral,
        bool isEligible)
    {
        var docs = new List<string> { "National ID", "Proof of income (3 months)" };
        if (requestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (hasCollateral) docs.Add("Collateral ownership deed");
        if (employmentMonths < 12) docs.Add("Employer letter");
        if (!isEligible) docs.Add("Manual underwriter referral form");
        return docs;
    }
}
