using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class StockMarket
    {
        [JsonProperty("ticker")]
        public string Ticker { get; set; }

#nullable enable
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("market")]
        public string? Market { get; set; }

        [JsonProperty("locale")]
        public string? Locale { get; set; }

        [JsonProperty("primary_exchange")]
        public string? PrimaryExchange { get; set; }
#nullable disable

    }
}
