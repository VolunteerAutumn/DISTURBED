using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
    }

    class Company
    {
        public string Name { get; set; }
        public DateTime TimeOfFounding { get; set; }
        public string Profile { get; set; }
        public string DirectorName { get; set; }
        public Employee[] Employees { get; set; }
        public string Address { get; set; }

        public int EmployeeCount => Employees?.Length ?? 0;

        public Company(string name, DateTime timeOfFounding, string profile, string directorName, Employee[] employees, string address)
        {
            Name = name;
            TimeOfFounding = timeOfFounding;
            Profile = profile;
            DirectorName = directorName;
            Employees = employees;
            Address = address;
        }

        public override string ToString()
        {
            return $"{Name}, {Profile}, {DirectorName}, {EmployeeCount} employees, {Address}";
        }
    }

    internal class Program
    {
        static Employee[] GenerateEmployees(int count, bool injectRussel = false)
        {
            Random random = new Random();

            string[] positions = { "Developer", "Manager", "Designer", "QA Analyst", "HR" };
            string[] names = { "Alex", "John", "Emma", "Michael", "Sophia", "Daniel", "Olivia", "David", "Kirk", "Haruka" };
            string[] surnames = { "Smith", "Jones", "Taylor", "Brown", "Wilson", "Miller", "Davis", "Venecia", "Martin", "Hashimoto" };

            List<Employee> employees = new();

            if (injectRussel)
            {
                employees.Add(new Employee
                {
                    Name = "Russel Morrigan",
                    Email = "volautyomani_208@gmail.com",
                    Position = null,
                    PhoneNumber = null,
                    Salary = 0
                });

                count--;
            }

            for (int i = 0; i < count; i++)
            {
                string fullName = $"{names[random.Next(names.Length)]} {surnames[random.Next(surnames.Length)]}";

                employees.Add(new Employee
                {
                    Name = fullName,
                    Position = positions[random.Next(positions.Length)],
                    PhoneNumber = $"+1-555-{random.Next(100, 999)}-{random.Next(1000, 9999)}",
                    Email = $"{fullName.ToLower().Replace(" ", ".")}@gmail.com",
                    Salary = random.Next(30000, 150000)
                });
            }

            return employees.ToArray();
        }

        static void PrintCompanies(string title, IEnumerable<Company> companies)
        {
            Console.WriteLine($"\n+---------------- {title} ----------------+");
            foreach (var company in companies)
            {
                Console.WriteLine(company);
            }
        }

        static void Main(string[] args)
        {
            Company[] companies =
            {
                new Company("White Food Ltd", new DateTime(2023, 1, 10), "Marketing", "John White", GenerateEmployees(1500), "London"),
                new Company("Tech Solutions", new DateTime(2020, 5, 15), "IT", "James Black", GenerateEmployees(2500, true), "New York"),
                new Company("Food Express", new DateTime(2024, 2, 1), "Logistics", "Anna Green", GenerateEmployees(800), "London"),
                new Company("Market Pro", new DateTime(2021, 7, 20), "Marketing", "Robert White", GenerateEmployees(1200), "Paris"),
                new Company("White Systems", DateTime.Now.AddDays(-123), "IT", "Michael Black", GenerateEmployees(5000), "London"),
                new Company("Concaptica", new DateTime(2026, 6, 22), "Social Engineering", "Pascal Dawroy", GenerateEmployees(7500), "Yunnan")
            };

            var russel = companies
                .SelectMany(c => c.Employees)
                .FirstOrDefault(e => e.Email == "volautyomani_208@gmail.com");

            if (russel != null)
            {
                Console.WriteLine("\n+---------------- RUSSEL FOUND ----------------+");
                Console.WriteLine($"Name: {russel.Name}");
                Console.WriteLine($"Email: {russel.Email}");
                Console.WriteLine($"Position: {russel.Position ?? "No Data"}");
                Console.WriteLine($"Phone: {russel.PhoneNumber ?? "No Data"}");
            }

            PrintCompanies("ALL", companies);
            PrintCompanies("FOOD", companies.Where(c => c.Name.Contains("Food")));
            PrintCompanies("MARKETING", companies.Where(c => c.Profile == "Marketing"));
            PrintCompanies("MARKETING OR IT", companies.Where(c => c.Profile == "Marketing" || c.Profile == "IT"));
            PrintCompanies(">100 EMPLOYEES", companies.Where(c => c.EmployeeCount > 100));
            PrintCompanies("100-300 EMPLOYEES", companies.Where(c => c.EmployeeCount >= 100 && c.EmployeeCount <= 300));
            PrintCompanies("LONDON", companies.Where(c => c.Address.Contains("London")));
            PrintCompanies("DIRECTOR WHITE", companies.Where(c => c.DirectorName.EndsWith("White")));
            PrintCompanies("OLDER THAN 2 YEARS", companies.Where(c => (DateTime.Now - c.TimeOfFounding).TotalDays > 730));
            PrintCompanies("FOUNDED 123 DAYS AGO", companies.Where(c => (DateTime.Now.Date - c.TimeOfFounding.Date).Days == 123));
            PrintCompanies("BLACK + WHITE", companies.Where(c => c.DirectorName.EndsWith("Black") && c.Name.Contains("White")));

            var highPaidEmployees = companies
                .Where(c => c.Name == "Tech Solutions")
                .SelectMany(c => c.Employees)
                .Where(e => e.Salary > 50000);

            var allManagers = companies
                .SelectMany(c => c.Employees)
                .Where(e => e.Position == "Manager");

            var phoneStarts23 = companies
                .SelectMany(c => c.Employees)
                .Where(e => e.PhoneNumber != null && (e.PhoneNumber.StartsWith("23") || e.PhoneNumber.StartsWith("+1-555-23")));

            var emailStartsDi = companies
                .SelectMany(c => c.Employees)
                .Where(e => e.Email != null && e.Email.StartsWith("di", StringComparison.OrdinalIgnoreCase));

            var lionelEmployees = companies
                .SelectMany(c => c.Employees)
                .Where(e => e.Name != null && (e.Name.StartsWith("Lionel ", StringComparison.OrdinalIgnoreCase) || e.Name == "Lionel"));

            Console.WriteLine($"\n+--- ALL MANAGERS (Found: {allManagers.Count()}) ---+");
            foreach (var emp in allManagers.Take(5))
            {
                Console.WriteLine($"- {emp.Name} | {emp.Position} | {emp.Salary}$");
            }

            Console.WriteLine($"\n+--- EMAIL STARTS WITH 'di' (Found: {emailStartsDi.Count()}) ---+");
            foreach (var emp in emailStartsDi.Take(5))
            {
                Console.WriteLine($"- {emp.Name} | Email: {emp.Email}");
            }
        }
    }
}
