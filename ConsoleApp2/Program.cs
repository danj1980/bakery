using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool orderComplete = false;
            
            var products = new Dictionary<string, decimal>
            {
                { "Cake", 0.50m },
                { "Loaf of Bread", 1.50m },
                { "Artisan Loaf", 2.50m }
            };

            var basket = new Dictionary<string, int>();

            while (!orderComplete)
            {
                Console.WriteLine("Please select from the following:- ");

                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Key} - {product.Value}");
                }

                Console.WriteLine("Please enter the name of the product you wish to add to the basket");
                Console.WriteLine("To end the sale, type Complete.");

                var itemName = Console.ReadLine();

                if (products.ContainsKey(itemName))
                {
                    //check if it's already in the basket
                    if (basket.ContainsKey(itemName))
                    { 
                        //it's already in the basket so add 1 to the quantity
                        basket[itemName]++;
                    } else
                    {
                        //it wasn't in the basket so now we add one
                        basket[itemName] = 1;
                    }
                    Console.Clear();
                    Console.WriteLine("Basket Updated");
                }
                else if (itemName == "Complete")
                {
                    orderComplete = true;
                }
                else
                {
                    Console.WriteLine("Product Not Found");
                }
            }

            decimal subTotal = 0;
            decimal discount = 0;
            decimal finalTotal = 0;

            Console.Clear();
            Console.WriteLine("ORDER COMPLETED");
            

            var requestedDate = DateTime.Now;
            bool validOrderTime = false;

            while (!validOrderTime)
            {
                Console.WriteLine("Please enter the date and time that the customer would like to collect the order which must be at least 48 hours in the future and between 9am and 5pm. Eg. DD/MM/YYYY HH:MM");

                var input = Console.ReadLine();

                if (DateTime.TryParse(input, out var date) == false)
                {
                    Console.WriteLine("You entered the date in an unknown format. Please try again");
                }
                else if (date < DateTime.Now.AddHours(48) || date.Hour < 9 || date.Hour > 17)
                {
                    Console.WriteLine("The time and date you entered was too early. Please try again allowing at least 48 hours for collection/delivery.");
                }
                else
                {
                    requestedDate = date;
                    validOrderTime = true;
                }
            }

            Console.Clear();

            Console.WriteLine("ORDER COMPLETED");
            Console.WriteLine("Here are the items ordered and the total cost.");

            foreach (var item in basket)
            {
                var lineTotal = products[item.Key] * item.Value;
                subTotal += lineTotal;

                Console.WriteLine($"{item.Key} x {item.Value} | {lineTotal.ToString("0.00")}");
            }

            if (subTotal >= 10)
            {
                discount = Math.Round(subTotal * 0.10m, 2);
            }

            finalTotal = subTotal - discount;

            Console.WriteLine("--");

            Console.WriteLine("SubTotal: £" + subTotal.ToString("0.00"));
            Console.WriteLine("Discount Applied: £" + discount.ToString("0.00"));
            Console.WriteLine("Total: £" + finalTotal.ToString("0.00"));

            Console.WriteLine("--");
            Console.WriteLine($"The order will be available on {requestedDate.ToString("dd/MM/yyyy")} at {requestedDate.ToString("HH:mm")}.");
            Console.WriteLine("--");
            Console.WriteLine("Press Return to End");
            Console.ReadLine();

        }
    }
}
