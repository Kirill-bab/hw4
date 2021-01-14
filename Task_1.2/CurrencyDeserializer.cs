using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library;
using Newtonsoft.Json;

namespace Task_1._2
{
    class CurrencyDeserializer
    {
        public static List<Currency> Currencies { get; private set; }

        public static void DeserializeCurrencies()
        {
            string json = "";
            try
            {
                json = FileWorker.Read("cache.json");   //reading the local attached file 
                var data = NbuAccess.SetCurrencies();    // if error occures here, local file data will be used
                FileWorker.Write("cache.json", data);   // overwriting(or creating) local file copy with new data from server               

                json = FileWorker.Read("cache.json");
            }
            catch (Exception e) when (e is AggregateException || e is HttpRequestException)    
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Program was uable to refresh currencies!");
                Console.WriteLine("Latest succesfull request results will be used");
                Console.WriteLine("----------------------------------------------");
            }

            Currencies = JsonConvert.DeserializeObject<List<Currency>>(json);
        }
    }
    
}
