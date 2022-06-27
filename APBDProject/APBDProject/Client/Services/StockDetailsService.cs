using APBDProject.Shared.DTOs;
using APBDProject.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public class StockDetailsService : IStockDetailsService
    {
        private const string key = "G1ocVvPGB0BWpUbn_9rhfKxrp6Uh3Dc3";
        private readonly HttpClient _httpClient;

        public StockDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddStockToDatabase(StockMarketDetails stock)
        {
            string result="";
            try
            {
                var response = await _httpClient.PostAsync($"/api/stocks/list", new StringContent(JsonConvert.SerializeObject(new StockPost
                {
                    Ticker = stock.Ticker,
                    Name = stock.Name,
                    Market = stock.Market,
                    Locale = stock.Locale,
                    PrimaryExchange = stock.PrimaryExchange,
                    Type = stock.Type,
                    CurrencyName = stock.CurrencyName,
                    SicDescription = stock.SicDescription,
                    City = stock.Addresss.City,
                    PhoneNumber = stock.PhoneNumber,
                    Description = stock.Description,
                    Logo = stock.Branding.LogoUrl,
                    Homepage = stock.Homepage

                }, Formatting.Indented), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                result = "OK";
            }
            catch (HttpRequestException e)
            {
                if(e.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    result = "CONFLICT";
                }
                else
                {
                    result = "ERROR";
                }
            }
            return result;
        }

        public async Task<StockMarketDetails> GetStockFromDatabase(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/stocks/{name}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                StockGet stockMarket = JObject.Parse(content).ToObject<StockGet>();

                return new StockMarketDetails
                {
                    Ticker = stockMarket.Ticker,
                    Name = stockMarket.Name,
                    Market = stockMarket.Market,
                    Locale = stockMarket.Locale,
                    PrimaryExchange = stockMarket.PrimaryExchange,
                    Type = stockMarket.Type,
                    CurrencyName = stockMarket.CurrencyName,
                    SicDescription = stockMarket.SicDescription,
                    Addresss = new AddressDetails
                    {
                        City = stockMarket.City
                    },
                    PhoneNumber = stockMarket.PhoneNumber,
                    Description = stockMarket.Description,
                    Branding = new BrandingDetails
                    {
                        LogoUrl = stockMarket.Logo
                    },
                    Homepage = stockMarket.Homepage
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StockMarketDetails> GetStockFromPolygon(string name)
        {
            var uri = $"https://api.polygon.io/v3/reference/tickers/{name}?apiKey={key}";
            StockMarketDetails stockMarkets = null;
            try
            {
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                stockMarkets = JObject.Parse(content).SelectToken("results").ToObject<StockMarketDetails>();
                Console.WriteLine(stockMarkets.Name);
            }
            catch (Exception)
            {
                stockMarkets = null;
            }
            return stockMarkets;
        }

        public async Task<string> UpdateStockInDatabase(StockMarketDetails stock)
        {
            string result = "";
            try
            {
                var response = await _httpClient.PutAsync($"/api/stocks/list/{stock.Ticker}", new StringContent(JsonConvert.SerializeObject(new StockPut
                {
                    Ticker = stock.Ticker,
                    Name = stock.Name,
                    Market = stock.Market,
                    Locale = stock.Locale,
                    PrimaryExchange = stock.PrimaryExchange,
                    Type = stock.Type,
                    CurrencyName = stock.CurrencyName,
                    SicDescription = stock.SicDescription,
                    City = stock.Addresss.City,
                    PhoneNumber = stock.PhoneNumber,
                    Description = stock.Description,
                    Logo = stock.Branding.LogoUrl,
                    Homepage = stock.Homepage

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
    }
}
