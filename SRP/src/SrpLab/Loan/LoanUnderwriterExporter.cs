namespace SrpLab.Loan;

/// <summary>Analytics CSV export schema — changes with reporting requirements.</summary>
public sealed class LoanUnderwriterExporter
{
    public string ExportRow(
        string applicationId,
        int creditScore,
        int employmentMonths,
        bool hasCollateral,
        decimal riskScore,
        bool isEligible) =>
        $"{applicationId},{creditScore},{employmentMonths},{(hasCollateral ? 1 : 0)},{riskScore:0.00},{(isEligible ? "Y" : "N")}";
}
