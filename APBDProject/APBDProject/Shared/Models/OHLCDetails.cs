using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class OHLCDetails
    {
        public string Ticker { get; set; }
        public DateTime Day { get; set; }

        [JsonProperty("o")]
        public double Open { get; set; }

        [JsonProperty("h")]
        public double High { get; set; }

        [JsonProperty("l")]
        public double Low { get; set; }

        [JsonProperty("c")]
        public double Close { get; set; }

        [JsonProperty("v")]
        public double Volume { get; set; }
    }
}
