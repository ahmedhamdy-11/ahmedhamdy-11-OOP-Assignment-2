using System;

namespace LibrarySystem
{
    public class Librarian : Staff
    {
        public Librarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary)
            : base(personId, fullName, phone, hireDate, monthlySalary, monthlyAllowance: 0m)
        {
        }

        // Processes returns; the rule itself (legal status transitions, dates) lives in Loan,
        // which is the class that owns that data.
        public void ProcessReturn(Loan loan, DateTime returnDate)
        {
            loan.Return(returnDate);
        }

        public void MarkItemLost(Loan loan)
        {
            loan.MarkAsLost();
        }
    }
}
