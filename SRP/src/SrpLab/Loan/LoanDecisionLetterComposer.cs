namespace SrpLab.Loan;

/// <summary>Legal/comms decision letter prose — changes with legal team.</summary>
public sealed class LoanDecisionLetterComposer
{
    public string Compose(
        string applicantName,
        decimal requestedAmount,
        decimal riskScore,
        bool isEligible,
        IReadOnlyList<string> requiredDocuments)
    {
        if (isEligible)
        {
            return $"Dear {applicantName},\nYour request for {requestedAmount:C} is pre-approved (risk {riskScore:0}).\n" +
                   $"Please upload: {string.Join("; ", requiredDocuments)}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {requestedAmount:C} at this time.\n" +
               $"Reference risk={riskScore:0}. You may reapply after improving documentation.\n";
    }
}
