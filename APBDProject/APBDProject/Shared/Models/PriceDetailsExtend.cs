using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class PriceDetailsExtend
    {
        public DateTime Day { get; set; }
        public string Ticker { get; set; }
#nullable enable
        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("afterHours")]
        public double? AfterHours { get; set; }

        [JsonProperty("preMarket")]
        public double? PreMarket { get; set; }
#nullable disable
    }
}
