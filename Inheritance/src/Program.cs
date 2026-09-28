using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== Library System Demo ===\n");

            // ------------------------------------------------------------------
            // 1) Things that must NOT compile (kept as comments on purpose).
            // ------------------------------------------------------------------
            // Person p = new Person("P1", "Nobody", "0100000000");           // must NOT compile: Person ctor is protected
            // Member m = new Member("M1", "Nobody", "0100000000", 3, 0m);    // must NOT compile: Member ctor is protected
            // Staff s = new Staff("S1", "Nobody", "0100000000", DateTime.Today, 5000m, 0m); // must NOT compile: Staff ctor is protected
            // LibraryItem li = new LibraryItem("C1", "Nothing", 1m, 10, 1m); // must NOT compile: LibraryItem ctor is protected

            StudentMember probeStudent = new StudentMember("M100", "Probe Student", "0101111111");
            // probeStudent.FullName = "New Name";                            // must NOT compile: FullName has no public setter (get-only)
            // probeStudent.Loans.Add(null);                                  // must NOT compile: Loans is IReadOnlyList<Loan>, has no Add
            Book probeBook = new Book("BK-PROBE", "Probe Book", 2m);
            // probeBook.IsOnLoan = true;                                     // must NOT compile: IsOnLoan has a private setter

            Console.WriteLine("Compile-time protections above are commented out on purpose (see source).\n");

            // ------------------------------------------------------------------
            // 2) Set up members, staff and items.
            // ------------------------------------------------------------------
            StudentMember studentMember = new StudentMember("M001", "Sara Ahmed", "0111111111");
            PremiumMember premiumMember = new PremiumMember("M002", "Omar Khaled", "0122222222", 10m);

            Librarian librarian = new Librarian("S001", "Nour Hassan", "0133333333", new DateTime(2019, 3, 1), 8000m);
            Shelver shelver = new Shelver("S002", "Kareem Adel", "0144444444", new DateTime(2021, 6, 15), 5500m, "Fiction");
            HeadLibrarian headLibrarian = new HeadLibrarian("S003", "Mona Fathy", "0155555555", new DateTime(2015, 1, 10), 12000m);

            Book book1 = new Book("BK-001", "Clean Code", 2m);
            Book book2 = new Book("BK-002", "The Pragmatic Programmer", 2m);
            DVD dvd1 = new DVD("DV-001", "Design Patterns - The Movie", 3m);
            Magazine magazine1 = new Magazine("MG-001", "National Geographic - Sept", 1m);

            // ------------------------------------------------------------------
            // 3) Borrow a withdrawn item -> rejected.
            // ------------------------------------------------------------------
            headLibrarian.WithdrawItem(magazine1);
            try
            {
                studentMember.Borrow(magazine1, DateTime.Today);
                Console.WriteLine("[UNEXPECTED] Borrowing a withdrawn item was allowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Rejected as expected] Borrow a withdrawn item: " + ex.Message);
            }
            headLibrarian.RestoreItem(magazine1);

            // ------------------------------------------------------------------
            // 4) Borrow an item already on loan -> rejected.
            // ------------------------------------------------------------------
            Loan firstLoanOfBook1 = studentMember.Borrow(book1, DateTime.Today);
            try
            {
                premiumMember.Borrow(book1, DateTime.Today);
                Console.WriteLine("[UNEXPECTED] Borrowing an item already on loan was allowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Rejected as expected] Borrow an item already on loan: " + ex.Message);
            }

            // ------------------------------------------------------------------
            // 5) Student member borrows a 4th item -> rejected (max is 3).
            // ------------------------------------------------------------------
            studentMember.Borrow(book2, DateTime.Today);
            studentMember.Borrow(dvd1, DateTime.Today);
            Book book3 = new Book("BK-003", "Refactoring", 2m);
            Book book4 = new Book("BK-004", "Domain-Driven Design", 2m);
            studentMember.Borrow(book3, DateTime.Today); // 3rd item, still allowed
            try
            {
                studentMember.Borrow(book4, DateTime.Today);
                Console.WriteLine("[UNEXPECTED] Student borrowing a 4th item was allowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Rejected as expected] Student borrows a 4th item: " + ex.Message);
            }

            Console.WriteLine();

            // ------------------------------------------------------------------
            // 6) Staff polymorphism through the base type - one list, no type checks.
            // ------------------------------------------------------------------
            List<Staff> staffList = new List<Staff>();
            staffList.Add(librarian);
            staffList.Add(shelver);
            staffList.Add(headLibrarian);

            Console.WriteLine("-- Monthly pay for every staff member --");
            foreach (Staff staffMember in staffList)
            {
                Console.WriteLine(staffMember.FullName + " monthly pay: " + staffMember.MonthlyPay.ToString("C"));
            }
            Console.WriteLine();

            // ------------------------------------------------------------------
            // 7) Item polymorphism through the base type - one list, no type checks.
            // ------------------------------------------------------------------
            List<LibraryItem> itemList = new List<LibraryItem>();
            itemList.Add(book1);
            itemList.Add(dvd1);
            itemList.Add(magazine1);

            Console.WriteLine("-- Loan period & daily late fee for every item --");
            foreach (LibraryItem item in itemList)
            {
                Console.WriteLine(item.Title + " | loan period: " + item.LoanPeriodDays + " days | daily late fee: " + item.DailyLateFee.ToString("C"));
            }
            Console.WriteLine();

            // ------------------------------------------------------------------
            // 8) Premium member returns a DVD 5 days late -> due date, fee, points.
            // ------------------------------------------------------------------
            DateTime dvdBorrowDate = new DateTime(2026, 9, 1);
            Loan dvdLoan = premiumMember.Borrow(dvd1, dvdBorrowDate);
            DateTime dvdReturnDate = dvdLoan.DueDate.AddDays(5);
            librarian.ProcessReturn(dvdLoan, dvdReturnDate);

            Console.WriteLine("-- Premium member returns a DVD 5 days late --");
            Console.WriteLine("Due date   : " + dvdLoan.DueDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("Return date: " + dvdReturnDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("Late fee   : " + dvdLoan.LateFee.ToString("C"));
            Console.WriteLine("Reading points so far: " + premiumMember.ReadingPoints);
            Console.WriteLine();

            // ------------------------------------------------------------------
            // 9) Return the same loan twice, and mark a returned loan as lost -> both rejected.
            // ------------------------------------------------------------------
            try
            {
                librarian.ProcessReturn(dvdLoan, dvdReturnDate);
                Console.WriteLine("[UNEXPECTED] Returning the same loan twice was allowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Rejected as expected] Return the same loan twice: " + ex.Message);
            }

            try
            {
                librarian.MarkItemLost(dvdLoan);
                Console.WriteLine("[UNEXPECTED] Marking a returned loan as lost was allowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Rejected as expected] Mark a returned loan as lost: " + ex.Message);
            }

            // ------------------------------------------------------------------
            // 10) A few extra rules for completeness: raises, reassigning, pricing.
            // ------------------------------------------------------------------
            Console.WriteLine("\n-- A few extra business rules --");
            try
            {
                librarian.GiveRaise(0m);
                Console.WriteLine("[UNEXPECTED] A zero-percent raise was allowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Rejected as expected] Give a zero-percent raise: " + ex.Message);
            }

            librarian.GiveRaise(10m);
            Console.WriteLine("Librarian pay after a 10% raise: " + librarian.MonthlyPay.ToString("C"));

            shelver.ReassignSection("Children");
            Console.WriteLine("Shelver reassigned to section: " + shelver.Section);

            try
            {
                headLibrarian.ChangeLateFeePrice(book1, 0m);
                Console.WriteLine("[UNEXPECTED] Setting a non-positive late fee was allowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Rejected as expected] Set a non-positive late fee: " + ex.Message);
            }

            headLibrarian.ChangeLateFeePrice(book1, 2.5m);
            Console.WriteLine("Book1 new daily late fee after pricing change: " + book1.DailyLateFee.ToString("C"));

            Console.WriteLine("\n=== Done ===");
        }
    }
}
