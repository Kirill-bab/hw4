using System;
using Library;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Task_1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Created by Kirill");
            Console.WriteLine("This program is a currency conventer");

            Console.WriteLine("====================================");
            var parameters = GetCurrencies();
            Console.WriteLine("====================================");

            try
            {
                CurrencyDeserializer.DeserializeCurrencies();
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Program was unable to found currencies file!");
                return;
            }

            Convert(parameters.Item1, parameters.Item2, parameters.Item3, CurrencyDeserializer.Currencies);

            Console.WriteLine("\n====================================");
        }

        private static (string,string,decimal) GetCurrencies()
        {
            Console.WriteLine("Please, enter Source currency:");
            var source = Console.ReadLine().Trim().ToUpper();

            while(source.Length != 3 || String.IsNullOrEmpty(source))
            {
                Console.WriteLine("Wrong currency format! Must be 3 Capital letters (UAH,USD,EUR,etc).");
                Console.WriteLine("Please, enter Source currency:");
                source = Console.ReadLine().Trim().ToUpper();
            }

            Console.WriteLine("Please, enter Desired currency:");
            var desired = Console.ReadLine().Trim().ToUpper();

            while (desired.Length != 3 || String.IsNullOrEmpty(desired))
            {
                Console.WriteLine("Wrong currency format! Must be 3 Capital letters (UAH,USD,EUR,etc).");
                Console.WriteLine("Please, enter Desired currency:");
                desired = Console.ReadLine().Trim().ToUpper();
            }

            Console.WriteLine("Please, enter Amount:");
            decimal amount;

            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0)
            {
                Console.WriteLine("Wrong amount format! Must be a positive decimal.");
                Console.WriteLine("Please, enter Amount:");
            }

            return (source, desired, amount);
        }

        private static void Convert(string from, string to, decimal amount, List<Currency> currencies)
        {
            Console.WriteLine();
            float exchangeRate;
            if(
                (currencies.Find(e=>e.Abreviation == from) == default(Currency) && from != "UAH")||
                (currencies.Find(e=>e.Abreviation == to) == default(Currency) && to != "UAH")
              )
            {
                Console.WriteLine($"Can't find pair [{from},{to}]!");
                return;
            }

            if (from == to) exchangeRate = 1;

            else if(from == "UAH") exchangeRate = 1 / currencies.Find(r => r.Abreviation == to).Rate;
      
            else if(to == "UAH") exchangeRate = currencies.Find(r => r.Abreviation == from).Rate;

            else
            {
                exchangeRate = currencies.Find(r => r.Abreviation == from).Rate / currencies.Find(r => r.Abreviation == to).Rate;
            }
            Console.WriteLine($"{amount} {from} x {exchangeRate} = {(decimal)exchangeRate * amount} {to} (on {currencies[0].ExchangeDate})");
        }
    }

}
