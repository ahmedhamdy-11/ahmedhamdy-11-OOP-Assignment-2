namespace LibrarySystem
{
    public class DVD : LibraryItem
    {
        // 7-day loan period; charges double the base fee (multiplier x2).
        public DVD(string catalogNumber, string title, decimal baseLateFee)
            : base(catalogNumber, title, baseLateFee, loanPeriodDays: 7, lateFeeMultiplier: 2m)
        {
        }
    }
}
