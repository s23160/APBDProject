using System;
using System.Collections.Generic;

namespace APBDProject.Server.Models
{
    public class Price
    {
        public int IdPrice { get; set; }
        public int IdStock { get; set; }
        public DateTime Day { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double? AfterHours { get; set; }
        public double? PreMarket { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
