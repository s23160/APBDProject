using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.DTOs
{
    public class UserWatchlistGet
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Market { get; set; }
        public string Locale { get; set; }
        public string PrimaryExchange { get; set; }
        public string Type { get; set; }
        public string CurrencyName { get; set; }
        public string SicDescription { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Homepage { get; set; }
    }
}
