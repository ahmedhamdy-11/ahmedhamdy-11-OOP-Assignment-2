using System;

namespace LibrarySystem
{
    public class Shelver : Staff
    {
        public string Section { get; private set; }

        public Shelver(string personId, string fullName, string phone, DateTime hireDate,
            decimal monthlySalary, string section)
            : base(personId, fullName, phone, hireDate, monthlySalary, monthlyAllowance: 0m)
        {
            if (string.IsNullOrEmpty(section))
                return;
            Section = section;
        }

        // Section can change only through this dedicated action.
        public void ReassignSection(string newSection)
        {
            if (string.IsNullOrEmpty(newSection))
                return;
            Section = newSection;
        }
    }
}
