using APBDProject.Shared.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public class SearchService : ISearchService
    {
        private const string key = "G1ocVvPGB0BWpUbn_9rhfKxrp6Uh3Dc3";
        private readonly HttpClient _httpClient;

        public SearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<StockMarket>> GetStockMarketsByName(string name)
        {
            var uri = $"https://api.polygon.io/v3/reference/tickers?search={name}&active=true&sort=ticker&order=asc&limit=100&apiKey={key}";
            IEnumerable<StockMarket> stockMarkets = null;
            try
            {
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                stockMarkets = JObject.Parse(content).SelectToken("results").ToObject<IEnumerable<StockMarket>>();
            }
            catch (Exception)
            {

            }
            return stockMarkets;
        }
    }
}
