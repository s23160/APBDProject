using APBDProject.Shared.DTOs;
using APBDProject.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public class WatchlistDetailsService : IWatchlistDetailsService
    {
        private const string key = "G1ocVvPGB0BWpUbn_9rhfKxrp6Uh3Dc3";
        private readonly HttpClient _httpClient;

        public WatchlistDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddStockToWatchlist(string name, string idUser)
        {
            string result = "";
            try
            {
                var response = await _httpClient.PostAsync($"/api/watchlists", new StringContent(JsonConvert.SerializeObject(new UserWatchlist
                {
                    IdUser = idUser,
                    Ticker = name

                }, Formatting.Indented), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                result = "OK";
            }
            catch (Exception)
            {
                result = "ERROR";
            }
            return result;
        }

        public async Task<IEnumerable<WatchlistDetails>> GetWatchlist(string user)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/watchlists/{user}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var stocksFromDatabase = JsonConvert.DeserializeObject<List<StockGet>>(content);

                List<WatchlistDetails> stocks = new List<WatchlistDetails>();
                foreach (var item in stocksFromDatabase)
                {
                    stocks.Add(new WatchlistDetails
                    {
                        Logo = item.Logo,
                        Ticker = item.Ticker,
                        Name = item.Name,
                        Locale = item.Locale,
                        Market = item.Market,

                    });
                }

                return stocks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> RemoveStockFromWatchlist(string ticker, string idUser)
        {
            string result = "";
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/watchlists/{ticker}/{idUser}");
                response.EnsureSuccessStatusCode();


                result = "OK";
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.Conflict)
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
    }
}
