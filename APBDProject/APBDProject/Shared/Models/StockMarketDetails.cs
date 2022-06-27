using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class StockMarketDetails
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

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("currency_name")]
        public string? CurrencyName { get; set; }

        [JsonProperty("sic_description")]
        public string? SicDescription { get; set; }

        [JsonProperty("address")]
        public AddressDetails? Addresss { get; set; }

        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("branding")]
        public BrandingDetails? Branding { get; set; }

        [JsonProperty("homepage_url")]
        public string? Homepage { get; set; }
#nullable disable
    }
}
