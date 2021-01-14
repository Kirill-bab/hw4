using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Task_1._2
{
    public class Currency
    {
        [JsonProperty("rate")]
        public float Rate { get; set; }

        [JsonProperty("cc")]
        public string Abreviation { get; set; }

        [JsonProperty("exchangedate")]
        public string ExchangeDate { get; set; }

        public Currency()
        {

        }

        public Currency( float rate, string cc, string exchangedate)
        {
            Rate = rate;
            Abreviation = cc;
            ExchangeDate = exchangedate;
        }
    }


}
