using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.DTOs
{
    public class PriceExtendPut
    {
        public DateTime Day { get; set; }
        public string Ticker { get; set; }
        public double? AfterHours { get; set; }
        public double? PreMarket { get; set; }
    }
}
