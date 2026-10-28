using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ConsoleApp5
{
    internal class Menu
    {
        private const string FilePath = "products.txt";

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine(" მენიუ ");
                Console.WriteLine("1. დავამატოთ პროდუქტი");
                Console.WriteLine("2. გამოვიტანოთ ყველა პროდუქტი კონსოლში");
                Console.WriteLine("3. გამოვიტანოთ კონსოლში პროდუქტი აიდის საშუალებით");
                Console.WriteLine("4. გამოვიტანოთ კონსოლში ფასის მიხედვით დასორტირებული პროდუქტები");
                Console.WriteLine("5. გამოსვლა");
                Console.Write("აირჩიეთ მოქმედება: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        ShowAllProducts();
                        break;
                    case "3":
                        ShowProductById();
                        break;
                    case "4":
                        ShowSortedProducts();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("არასწორი არჩევანი, სცადეთ თავიდან.");
                        break;
                }
            }
        }

        private void AddProduct()
        {
            Console.Write("შეიყვანეთ ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("შეიყვანეთ სახელი: ");
            string name = Console.ReadLine();

            Console.Write("შეიყვანეთ ფასი: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("შეიყვანეთ აღწერა: ");
            string description = Console.ReadLine();

            Product product = new Product(id, name, price, description);

            
            File.AppendAllLines(FilePath, new[] { product.ToFileString() });
            Console.WriteLine("პროდუქტი წარმატებით დაემატა!");
        }

        private void ShowAllProducts()
        {
            var products = LoadProducts();
            if (products.Count == 0)
            {
                Console.WriteLine("ფაილი ცარიელია.");
                return;
            }

            foreach (var p in products)
            {
                Console.WriteLine(p);
            }
        }

        private void ShowProductById()
        {
            Console.Write("შეიყვანეთ საძიებო ID: ");
            int searchId = int.Parse(Console.ReadLine());

            var products = LoadProducts();
            var product = products.FirstOrDefault(p => p.Id == searchId);

            if (product != null)
            {
                Console.WriteLine(product);
            }
            else
            {
                Console.WriteLine("პროდუქტი ამ ID-ით ვერ მოიძებნა.");
            }
        }

        private void ShowSortedProducts()
        {
            var products = LoadProducts();
            if (products.Count == 0)
            {
                Console.WriteLine("ფაილი ცარიელია.");
                return;
            }

            var sorted = products.OrderBy(p => p.Price).ToList();
            foreach (var p in sorted)
            {
                Console.WriteLine(p);
            }
        }

        private List<Product> LoadProducts()
        {
            List<Product> products = new List<Product>();
            if (!File.Exists(FilePath)) return products;

            foreach (var line in File.ReadAllLines(FilePath))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var p = Product.FromFileString(line);
                    if (p != null) products.Add(p);
                }
            }
            return products;
        }
    }
}
