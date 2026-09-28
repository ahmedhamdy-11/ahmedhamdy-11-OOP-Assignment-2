namespace LibrarySystem
{
    public class PremiumMember : Member
    {
        // Premium: up to 10 items on loan, a discount decided at join time and never changed.
        public PremiumMember(string personId, string fullName, string phone, decimal discountPercentage)
            : base(personId, fullName, phone, maxLoans: 10, discountPercentage: discountPercentage)
        {
        }

        // 5 reading points per returned loan. Computed, never typed in by hand.
        public int ReadingPoints
        {
            get
            {
                int returnedCount = 0;
                foreach (Loan loan in Loans)
                {
                    if (loan.Status == LoanStatus.Returned)
                    {
                        returnedCount++;
                    }
                }
                return returnedCount * 5;
            }
        }
    }
}
