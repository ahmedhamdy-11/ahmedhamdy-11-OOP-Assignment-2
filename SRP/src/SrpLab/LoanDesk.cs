using SrpLab.Loan;

namespace SrpLab;

/// <summary>
/// Loan desk: coordinates risk assessment, document checklists, and decision communications.
/// </summary>
public sealed class LoanDesk
{
    private readonly LoanRiskCalculator _riskCalculator = new();
    private readonly LoanDocumentChecklist _documentChecklist = new();
    private readonly LoanDecisionLetterComposer _letterComposer = new();
    private readonly LoanUnderwriterExporter _underwriterExporter = new();

    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    public LoanDesk(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }

    public decimal RiskScore() =>
        _riskCalculator.Calculate(RequestedAmount, CreditScore, EmploymentMonths, HasCollateral);

    public bool IsEligible() => _riskCalculator.IsEligible(RiskScore(), CreditScore);

    public IReadOnlyList<string> RequiredDocuments() =>
        _documentChecklist.Required(RequestedAmount, EmploymentMonths, HasCollateral, IsEligible());

    public string DecisionLetter(string applicantName) =>
        _letterComposer.Compose(applicantName, RequestedAmount, RiskScore(), IsEligible(), RequiredDocuments());

    public string UnderwriterCsvRow(string applicationId) =>
        _underwriterExporter.ExportRow(applicationId, CreditScore, EmploymentMonths, HasCollateral, RiskScore(), IsEligible());
}
