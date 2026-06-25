using System;
using System.Linq;

namespace ConsoleApp1
{
    class Phone
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Phone[] phones =
            {
                new Phone { Name = "Galaxy S24", Manufacturer = "Samsung", Price = 850, ReleaseDate = new DateTime(2024, 1, 17) },
                new Phone { Name = "iPhone 15", Manufacturer = "Apple", Price = 999, ReleaseDate = new DateTime(2023, 9, 22) },
                new Phone { Name = "Redmi Note 13", Manufacturer = "Xiaomi", Price = 300, ReleaseDate = new DateTime(2024, 1, 15) },
                new Phone { Name = "Galaxy A54", Manufacturer = "Samsung", Price = 450, ReleaseDate = new DateTime(2023, 3, 15) },
                new Phone { Name = "Nokia 3310", Manufacturer = "Nokia", Price = 50, ReleaseDate = new DateTime(2000, 9, 1) }
            };

            Console.WriteLine("Number of phones: " + phones.Count());
            Console.WriteLine("Number of phones with price greater than 100: " + phones.Count(p => p.Price > 100));
            Console.WriteLine("Number of phones with price between 400 and 700: " + phones.Count(p => p.Price >= 400 && p.Price <= 700));
            Console.WriteLine("Number of Samsung phones: " + phones.Count(p => p.Manufacturer == "Samsung"));

            Phone minPricePhone = phones.OrderBy(p => p.Price).First();
            Phone maxPricePhone = phones.OrderByDescending(p => p.Price).First();
            Phone oldestPhone = phones.OrderBy(p => p.ReleaseDate).First();
            Phone newestPhone = phones.OrderByDescending(p => p.ReleaseDate).First();

            Console.WriteLine("\nPhone with minimum price:");
            Console.WriteLine($"{minPricePhone.Name}, {minPricePhone.Manufacturer}, {minPricePhone.Price}, {minPricePhone.ReleaseDate:d}");

            Console.WriteLine("\nPhone with maximum price:");
            Console.WriteLine($"{maxPricePhone.Name}, {maxPricePhone.Manufacturer}, {maxPricePhone.Price}, {maxPricePhone.ReleaseDate:d}");

            Console.WriteLine("\nOldest phone:");
            Console.WriteLine($"{oldestPhone.Name}, {oldestPhone.Manufacturer}, {oldestPhone.Price}, {oldestPhone.ReleaseDate:d}");

            Console.WriteLine("\nNewest phone:");
            Console.WriteLine($"{newestPhone.Name}, {newestPhone.Manufacturer}, {newestPhone.Price}, {newestPhone.ReleaseDate:d}");

            Console.WriteLine("\nAverage phone price: " + phones.Average(p => (double)p.Price));
        }
    }
}
