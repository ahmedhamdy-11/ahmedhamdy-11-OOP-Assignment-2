namespace LibrarySystem
{
    public class Book : LibraryItem
    {
        // 21-day loan period; charges the base fee as is (multiplier x1).
        public Book(string catalogNumber, string title, decimal baseLateFee)
            : base(catalogNumber, title, baseLateFee, loanPeriodDays: 21, lateFeeMultiplier: 1m)
        {
        }
    }
}
