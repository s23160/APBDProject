using APBDProject.Shared.DTOs;
using APBDProject.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public class PriceDetailsService : IPriceDetailsService
    {
        private const string key = "G1ocVvPGB0BWpUbn_9rhfKxrp6Uh3Dc3";
        private readonly HttpClient _httpClient;

        public PriceDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddPriceExtendToDatabase(PriceDetailsExtend price)
        {
            string result = "";
            try
            {
                var response = await _httpClient.PutAsync($"/api/prices/list/{price.Ticker}", new StringContent(JsonConvert.SerializeObject(new PriceExtendPut
                {
                    Day = price.Day,
                    Ticker = price.Ticker,
                    AfterHours = price.AfterHours,
                    PreMarket = price.PreMarket

                }, Formatting.Indented), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                result = "OK";
            }
            catch (HttpRequestException)
            {
                result = "ERROR";
            }
            return result;
        }

        public async Task<string> AddPricesToDatabase(List<OHLCDetails> ohlc)
        {
            string result = "";
            try
            {
                PriceListPost priceListPost = new PriceListPost();
                priceListPost.Prices = new List<PricePost>();
                foreach(var item in ohlc)
                {
                    priceListPost.Prices.Add(new PricePost
                    {
                        Ticker = item.Ticker,
                        Day = item.Day.Date,
                        Open = item.Open,
                        High = item.High,
                        Low = item.Low,
                        Close = item.Close,
                        Volume = item.Volume,
                    });

                }
                var response = await _httpClient.PostAsync($"/api/prices/list", new StringContent(JsonConvert.SerializeObject(priceListPost, Formatting.Indented), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                result = "OK";
            }
            catch (HttpRequestException)
            {
                result = "ERROR";
            }
            return result;
        }

        public async Task<PriceDetailsExtend> GetPriceExtendFromDatabase(string name, DateTime day)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/prices/{name}/{day}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var priceExtendFromDatabase = JsonConvert.DeserializeObject<PriceGet>(content);

                return new PriceDetailsExtend
                {
                    Day = day,
                    Ticker = name,
                    Open = priceExtendFromDatabase.Open,
                    High = priceExtendFromDatabase.High,
                    Low = priceExtendFromDatabase.Low,
                    Close = priceExtendFromDatabase.Close,
                    Volume = priceExtendFromDatabase.Volume,
                    AfterHours = priceExtendFromDatabase.AfterHours,
                    PreMarket = priceExtendFromDatabase.PreMarket
                };
            }
            catch (Exception)
            {
                return null;
            }
         }

        public async Task<PriceDetailsExtend> GetPriceExtendFromPolygon(string name, DateTime day)
        {
            try
            {
                var date = day.ToString("yyyy-MM-dd");

                var response = await _httpClient.GetAsync($"https://api.polygon.io/v1/open-close/{name}/{date}?adjusted=true&apiKey={key}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var priceExtendFromPolygon = JObject.Parse(content).ToObject<PriceDetailsExtend>();

                priceExtendFromPolygon.Ticker = name;
                priceExtendFromPolygon.Day = day;
                return priceExtendFromPolygon;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<OHLCDetails>> GetPricesFromDatabase(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/prices/{name}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var pricesFromDatabase = JsonConvert.DeserializeObject<List<PriceGet>>(content);

                List<OHLCDetails> prices = new List<OHLCDetails>();
                foreach (var item in pricesFromDatabase)
                {
                    prices.Add(new OHLCDetails
                    {
                        Day = item.Day,
                        Open = item.Open,
                        High = item.High,
                        Low = item.Low,
                        Close = item.Close,
                        Volume = item.Volume
                    });

                }

                return prices;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<OHLCDetails>> GetPricesFromPolygon(string name)
        {
            try
            {
                var dateEnd = DateTime.Now.ToString("yyyy-MM-dd");
                var dateStart = DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd");

                var response = await _httpClient.GetAsync($"https://api.polygon.io/v2/aggs/ticker/{name}/range/1/day/{dateStart}/{dateEnd}?adjusted=true&sort=desc&limit=90&apiKey={key}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var pricesFromPolygon = JObject.Parse(content).SelectToken("results").ToObject<IEnumerable<OHLCDetails>>();


                int count = 0;
                foreach (var item in pricesFromPolygon)
                {
                    item.Ticker = name;
                    item.Day = DateTime.Now.AddDays(count).Date;
                    count--;
                }

                return pricesFromPolygon;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
