namespace LibrarySystem
{
    public class StudentMember : Member
    {
        // Student: at most 3 items on loan, no discount.
        public StudentMember(string personId, string fullName, string phone)
            : base(personId, fullName, phone, maxLoans: 3, discountPercentage: 0m)
        {
        }
    }
}
