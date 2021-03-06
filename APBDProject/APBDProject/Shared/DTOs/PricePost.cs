using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.DTOs
{
    public class PricePost
    {
        public string Ticker { get; set; }
        public DateTime Day { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double? AfterHours { get; set; }
        public double? PreMarket { get; set; }
    }

    public class PriceListPost
    {
        public List<PricePost> Prices { get; set; }
    }
}
