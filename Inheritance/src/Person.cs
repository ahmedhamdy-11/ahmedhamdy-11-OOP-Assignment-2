using System;

namespace LibrarySystem
{
    // Nobody is ever registered as "just a person" -> protected constructor,
    // only Member and Staff (and their own children) can call it.
    public class Person
    {
        public string PersonId { get; }
        public string FullName { get; }
        public string Phone { get; }

        protected Person(string personId, string fullName, string phone)
        {
            if (string.IsNullOrEmpty(personId))
                return;
            if (string.IsNullOrEmpty(fullName))
                return;
            if (string.IsNullOrEmpty(phone))
            return;

            PersonId = personId;
            FullName = fullName;
            Phone = phone;
        }
    }
}
