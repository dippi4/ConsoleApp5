using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    internal class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Product(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }

       
        public string ToFileString()
        {
            return $"{Id}|{Name}|{Price}|{Description}";
        }

       
        public static Product FromFileString(string line)
        {
            var parts = line.Split('|');
            if (parts.Length == 4)
            {
                return new Product(
                    int.Parse(parts[0]),
                    parts[1],
                    decimal.Parse(parts[2]),
                    parts[3]
                );
            }
            return null;
        }

        public override string ToString()
        {
            return $"ID: {Id}, სახელი: {Name}, ფასი: {Price}, აღწერა: {Description}";
        }
    }
}
