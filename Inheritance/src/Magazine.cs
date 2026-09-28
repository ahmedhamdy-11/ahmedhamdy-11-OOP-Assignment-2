namespace LibrarySystem
{
    public class Magazine : LibraryItem
    {
        // 3-day loan period; charges half the base fee (multiplier x0.5).
        public Magazine(string catalogNumber, string title, decimal baseLateFee)
            : base(catalogNumber, title, baseLateFee, loanPeriodDays: 3, lateFeeMultiplier: 0.5m)
        {
        }
    }
}
