using System;

namespace LibrarySystem
{
    public class Loan
    {
        private static int _nextLoanId = 1;

        public int LoanId { get; }
        public DateTime BorrowDate { get; }
        public Member Member { get; }
        public LibraryItem Item { get; }

        public LoanStatus Status { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        // Never typed in - always calculated from the borrow date and the item's own loan period.
        public DateTime DueDate
        {
            get
            {
                return BorrowDate.AddDays(Item.LoanPeriodDays);
            }
        }

        // days late x item's daily late fee, minus the member's discount. Zero if on time / not yet returned.
        public decimal LateFee
        {
            get
            {
                if (Status != LoanStatus.Returned || ReturnDate == null)
                {
                    return 0m;
                }

                int daysLate = (ReturnDate.Value.Date - DueDate.Date).Days;
                if (daysLate <= 0)
                {
                    return 0m;
                }

                decimal rawFee = daysLate * Item.DailyLateFee;
                decimal discounted = rawFee * (1 - Member.DiscountPercentage / 100m);
                if (discounted < 0)
                {
                    return 0m;
                }
                return discounted;
            }
        }

        // Only Member.Borrow is meant to create loans (and it immediately adds the
        // loan to its own history), which keeps "adding to history" a member action.
        internal Loan(Member member, LibraryItem item, DateTime borrowDate)
        {
            if (item.IsWithdrawn)
                return;
            if (item.IsOnLoan)
                return;

            LoanId = _nextLoanId++;
            Member = member;
            Item = item;
            BorrowDate = borrowDate;
            Status = LoanStatus.Borrowed;
            ReturnDate = null;

            item.MarkAsOnLoan();
        }

        public void Return(DateTime returnDate)
        {
            if (Status != LoanStatus.Borrowed)
                return;
            if (returnDate.Date < BorrowDate.Date)
                return;

            ReturnDate = returnDate;
            Status = LoanStatus.Returned;
            Item.MarkAsReturned();
        }

        public void MarkAsLost()
        {
            if (Status != LoanStatus.Borrowed)
                return;

            Status = LoanStatus.Lost;
        }
    }
}
