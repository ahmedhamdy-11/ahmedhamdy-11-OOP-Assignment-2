using System;

namespace LibrarySystem
{
    // Nobody is ever registered as "just an item" -> protected constructor.
    // Only Book, DVD and Magazine can be created.
    public class LibraryItem
    {
        public string CatalogNumber { get; }
        public string Title { get; }

        public decimal BaseLateFee { get; private set; }

        // Loan period and the late-fee multiplier differ per kind and are passed
        // up through base(...) instead of an "is"/switch on the concrete type.
        public int LoanPeriodDays { get; }
        private readonly decimal _lateFeeMultiplier;

        public bool IsWithdrawn { get; private set; }

        // Not settable from outside at all (private set). It only changes as a side
        // effect of borrowing/returning, through the two internal methods below,
        // which only Loan (its own module) is meant to call.
        public bool IsOnLoan { get; private set; }

        // What we actually charge per late day: base fee adjusted by the kind's multiplier.
        // Whoever needs it just reads this property - no type check required.
        public decimal DailyLateFee
        {
            get
            {
                return BaseLateFee * _lateFeeMultiplier;
            }
        }

        protected LibraryItem(string catalogNumber, string title, decimal baseLateFee,
            int loanPeriodDays, decimal lateFeeMultiplier)
        {
            if (string.IsNullOrEmpty(catalogNumber))
            {
                return ;
            }
            if (string.IsNullOrEmpty(title))
            {
                return;
            }
            if (baseLateFee <= 0)
            {
                return;
            }

            CatalogNumber = catalogNumber;
            Title = title;
            BaseLateFee = baseLateFee;
            LoanPeriodDays = loanPeriodDays;
            _lateFeeMultiplier = lateFeeMultiplier;
            IsWithdrawn = false;
            IsOnLoan = false;
        }

        // Fees change only through this dedicated pricing action.
        public void SetBaseLateFee(decimal newFee)
        {
            if (newFee <= 0)
            {
                return;
            }
            BaseLateFee = newFee;
        }

        public void Withdraw()
        {
            IsWithdrawn = true;
        }

        public void Restore()
        {
            IsWithdrawn = false;
        }

        // Side effects of borrowing/returning only - called exclusively from Loan.
        internal void MarkAsOnLoan()
        {
            IsOnLoan = true;
        }

        internal void MarkAsReturned()
        {
            IsOnLoan = false;
        }
    }
}
