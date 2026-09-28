using System;

namespace LibrarySystem
{
    // Nobody is ever registered as "just staff" -> protected constructor.
    // Only Librarian, Shelver and HeadLibrarian can be created.
    public class Staff : Person
    {
        public DateTime HireDate { get; }
        public decimal MonthlySalary { get; private set; }

        // The fixed allowance (0 for most staff, 400 for the Head Librarian) is
        // handed up through base(...), so MonthlyPay works for every child
        // with no "is"/switch on the concrete type.
        public decimal MonthlyAllowance { get; }

        public decimal MonthlyPay
        {
            get
            {
                return MonthlySalary + MonthlyAllowance;
            }
        }

        protected Staff(string personId, string fullName, string phone, DateTime hireDate,
            decimal monthlySalary, decimal monthlyAllowance)
            : base(personId, fullName, phone)
        {
            HireDate = hireDate;
            MonthlySalary = monthlySalary;
            MonthlyAllowance = monthlyAllowance;
        }

        // Salary can change only through this action; a raise can never be zero or negative.
        public void GiveRaise(decimal percentage)
        {
            if (percentage <= 0)
            {

                return;
            }
            MonthlySalary = MonthlySalary * (1 + percentage / 100m);
        }
    }
}
