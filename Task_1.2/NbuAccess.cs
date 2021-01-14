using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;


namespace Task_1._2
{
    public class NbuAccess
    {
        private static async Task<string> PrivateGetRates()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(@"https://bank.gov.ua/"),
                Timeout = new TimeSpan(0, 0, 10)
            };
           
            var response = await client.GetAsync("NBUStatService/v1/statdirectory/exchange?json");
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync();

            return json.Result;
        }
                   
        public static string SetCurrencies()
        {
            return PrivateGetRates().Result;
        }
        
            
    }
}
