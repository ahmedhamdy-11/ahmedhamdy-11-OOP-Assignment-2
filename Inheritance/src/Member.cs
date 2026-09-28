using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibrarySystem
{
    // Nobody is ever registered as "just a member" -> protected constructor.
    // Only StudentMember and PremiumMember can be created.
    public class Member : Person
    {
        // Values that differ per kind of member (max loans, discount) are
        // passed up through base(...) instead of being checked with "is"/switch.
        public int MaxLoans { get; }
        public decimal DiscountPercentage { get; }

        private readonly List<Loan> _loans = new List<Loan>();

        // Exposed as read-only so outside code cannot Add/Remove through it.
        public IReadOnlyList<Loan> Loans
        {
            get
            {
                return new ReadOnlyCollection<Loan>(_loans);
            }
        }

        protected Member(string personId, string fullName, string phone, int maxLoans, decimal discountPercentage)
            : base(personId, fullName, phone)
        {
            MaxLoans = maxLoans;
            DiscountPercentage = discountPercentage;
        }

        // Only the member's own action may add a loan to its history.
        public Loan Borrow(LibraryItem item, DateTime borrowDate)
        {
            int activeLoans = 0;
            foreach (Loan existingLoan in _loans)
            {
                if (existingLoan.Status == LoanStatus.Borrowed)
                {
                    activeLoans++;
                }
            }

            if (activeLoans >= MaxLoans)
                return null; // Member has reached the maximum number of active loans.

            // Loan's own constructor enforces "not withdrawn" and "not already on loan".
            Loan loan = new Loan(this, item, borrowDate);
            _loans.Add(loan);
            return loan;
        }
    }
}
