using CodeChallengeLuxoft.Models;
using CodeChallengeLuxoft.BusinessLogic;
using CodeChallengeLuxoft.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallengeLuxoft
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the newest Luxoft POS");
            do
            {
                try
                {
                    //Prints the current currency/country the POS is working on which is defined in a setting on the App.config
                    Console.WriteLine($"Country: {Configurations.Country}");
                    Console.WriteLine($"Currency: {Configurations.Currency}");
                    Console.WriteLine($"Accepted bills and coins: {string.Join(", ", Configurations.AvailableCurrency.Select(x => x.Value))}");
                    ProcessSale();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }

                Console.WriteLine("Press ESCAPE to exit or any other key to continue");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static void ProcessSale()
        {
            Console.WriteLine("Please write the total amount for the items(s)");
            var inputTotal = Console.ReadLine();
            decimal total = SaleProcess.ValidateTotal(inputTotal);
            
            Console.WriteLine("Please write the bills and coins in the following format"
                            + Environment.NewLine + "Quantity$Bill, Quantity$Bill"
                            + Environment.NewLine + "So if the customer gives you two $5.00 bill and one $0.50 coin the input would be like this:"
                            + Environment.NewLine + "2$5.00, 1$0.50"
                            + Environment.NewLine + "Amounts given by the customer:");
            var money = Console.ReadLine();
            var payment = SaleProcess.GetPayment(money);
            SaleProcess.ValidateAmounts(total, payment);
            var change = SaleProcess.GetChange(total, payment);
            Console.WriteLine($"Total Change is: {change.Select(x => x.Value * x.Quantity).Sum()},{Environment.NewLine}Please give the following bills and coins:");
            Console.WriteLine($"{string.Join(", ", change.Select(x => $"{x.Quantity} of ${x.Value}"))}");
        }
    }
}
