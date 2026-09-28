using System;

namespace LibrarySystem
{
    public class HeadLibrarian : Staff
    {
        // Head Librarian also gets a fixed 400 responsibility allowance.
        public HeadLibrarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary)
            : base(personId, fullName, phone, hireDate, monthlySalary, monthlyAllowance: 400m)
        {
        }

        public void ChangeLateFeePrice(LibraryItem item, decimal newFee)
        {
            item.SetBaseLateFee(newFee);
        }

        public void WithdrawItem(LibraryItem item)
        {
            item.Withdraw();
        }

        public void RestoreItem(LibraryItem item)
        {
            item.Restore();
        }
    }
}
