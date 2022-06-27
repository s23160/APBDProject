using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class OHLC
    {
        [JsonProperty("results")]
        public List<OHLCDetails> Results { get; set; }
    }
}
