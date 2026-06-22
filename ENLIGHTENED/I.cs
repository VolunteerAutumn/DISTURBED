using System;
using System.Linq;

namespace ConsoleApp1
{
    class Company
    {
        public string Name { get; set; }
        public DateTime TimeOfFounding { get; set; }
        public string Profile { get; set; }
        public string DirectorName { get; set; }
        public int EmployeeCount { get; set; }
        public string Address {  get; set; }

        public Company(string name, DateTime tof, string profile, string directorName, int employeeCount, string address)
        {
            Name = name;
            TimeOfFounding = tof;
            Profile = profile;
            DirectorName = directorName;
            EmployeeCount = employeeCount;
            Address = address;
        }
        public Company() {
            Name = "None";
            TimeOfFounding = DateTime.Now;
            Profile = "None";
            DirectorName = "None";
            EmployeeCount = 0;
            Address = "None";
        }

        public override string ToString()
        {
            return $"{Name}, {Profile}, {DirectorName}, {EmployeeCount} employees, {Address}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Company[] companies =
            {
                new Company("White Food Ltd", new DateTime(2023, 1, 10), "Marketing", "John White", 150, "London"),

                new Company("Tech Solutions", new DateTime(2020, 5, 15), "IT", "James Black", 250, "New York"),

                new Company("Food Express", new DateTime(2024, 2, 1), "Logistics", "Anna Green", 80, "London"),

                new Company("Market Pro", new DateTime(2021, 7, 20), "Marketing", "Robert White", 120, "Paris"),

                new Company("White Systems", DateTime.Now.AddDays(-123), "IT", "Michael Black", 500, "London"),
                
                new Company("Concaptica", new DateTime(2026, 6, 22), "Social Engineering", "Pascal Dawroy", 7500, "Yunnan")
            };

            var allCompanies = companies;

            var foodCompanies = companies.Where(c => c.Name.Contains("Food"));

            var marketingCompanies = companies.Where(c => c.Profile == "Marketing");

            var marketingOrIT = companies.Where(c => c.Profile == "Marketing" || c.Profile == "IT");

            var moreThan100 = companies.Where(c => c.EmployeeCount > 100);

            var between100And300 = companies.Where(c => c.EmployeeCount >= 100 && c.EmployeeCount <= 300);

            var londonCompanies = companies.Where(c => c.Address.Contains("London"));

            var directorWhite = companies.Where(c => c.DirectorName.Split(' ').Last() == "White");

            var olderThan2Years = companies.Where(c => (DateTime.Now - c.TimeOfFounding).TotalDays > 365 * 2);

            var founded123DaysAgo = companies.Where(c => (DateTime.Now.Date - c.TimeOfFounding.Date).Days == 123);

            var blackDirectorWhiteName = companies.Where(c => c.DirectorName.Split(' ').Last() == "Black" && c.Name.Contains("White"));
            Console.WriteLine("\n+-------------------- A L L ----------------------------+");
            foreach (var company in allCompanies)
                Console.WriteLine(company);

            Console.WriteLine("\n+-------------------- F O O D --------------------------+");
            foreach (var company in foodCompanies)
                Console.WriteLine(company);

            Console.WriteLine("\n+------------------ M A R K E T I N G ------------------+");
            foreach (var company in marketingCompanies)
                Console.WriteLine(company);

            Console.WriteLine("\n+------------ M A R K E T I N G   O R   I T ------------+");
            foreach (var company in marketingOrIT)
                Console.WriteLine(company);

            Console.WriteLine("\n+-------------- > 1 0 0   E M P L O Y E E S ------------+");
            foreach (var company in moreThan100)
                Console.WriteLine(company);

            Console.WriteLine("\n+----------- 1 0 0 - 3 0 0   E M P L O Y E E S ---------+");
            foreach (var company in between100And300)
                Console.WriteLine(company);

            Console.WriteLine("\n+-------------------- L O N D O N ----------------------+");
            foreach (var company in londonCompanies)
                Console.WriteLine(company);

            Console.WriteLine("\n+--------------------- W H I T E -----------------------+");
            foreach (var company in directorWhite)
                Console.WriteLine(company);

            Console.WriteLine("\n+--------------------- OLDER THAN 2Y -------------------+");
            foreach (var company in olderThan2Years)
                Console.WriteLine(company);

            Console.WriteLine("\n+-------------- FOUNDED 123 DAYS -----------+"); //LAZY
            foreach (var company in founded123DaysAgo)
                Console.WriteLine(company);

            Console.WriteLine("\n+---------- BLACK + WHITE IN NAME ----------+");
            foreach (var company in blackDirectorWhiteName)
                Console.WriteLine(company);
        }
    }
}
