namespace SrpLab.Course;

/// <summary>Tuition invoice line formatting — changes with finance/tax rules.</summary>
public sealed class TuitionInvoiceFormatter
{
    public string Format(string courseCode, bool isSeated, decimal tuition)
    {
        if (!isSeated) return $"{courseCode},WAITLIST,0.00";
        var vat = Math.Round(tuition * 0.14m, 2);
        return $"{courseCode},TUITION,{tuition:0.00},VAT,{vat:0.00},TOTAL,{(tuition + vat):0.00}";
    }
}
