using APBDProject.Shared.DTOs;
using APBDProject.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public class ArticleDetailsService : IArticleDetailsService
    {
        private const string key = "G1ocVvPGB0BWpUbn_9rhfKxrp6Uh3Dc3";
        private readonly HttpClient _httpClient;

        public ArticleDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddArticlesToDatabase(string ticker, IEnumerable<ArticleDetails> articleList)
        {
            string result = "";
            try
            {
                ArticleListPost articleListPost = new ArticleListPost();
                articleListPost.Articles = new List<ArticlePost>();
                foreach (var item in articleList)
                {
                    articleListPost.Articles.Add(new ArticlePost
                    {
                        Ticker =  ticker,
                        IdentifierName = item.IdentifierName,
                        Author = item.Author,
                        Title = item.Title,
                        Description = item.Description,
                        PublishedDate = item.PublishedDate,
                        ImageUrl = item.ImageUrl
                    });

                }
                var response = await _httpClient.PostAsync($"/api/articles/list", new StringContent(JsonConvert.SerializeObject(articleListPost, Formatting.Indented), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                result = "OK";
            }
            catch (HttpRequestException)
            {
                result = "ERROR";
            }
            return result;
        }

        public async Task<IEnumerable<ArticleDetails>> GetArticlesFromDatabase(string ticker)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/articles/{ticker}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var articlesFromDatabase = JsonConvert.DeserializeObject<List<ArticleGet>>(content);

                List<ArticleDetails> articlesList = new List<ArticleDetails>();
                foreach (var item in articlesFromDatabase)
                {
                    articlesList.Add(new ArticleDetails
                    {
                        IdentifierName = item.IdentifierName,
                        Author = item.Author,
                        Title = item.Title,
                        Description = item.Description,
                        PublishedDate = item.PublishedDate,
                        ImageUrl = item.ImageUrl
                    });

                }

                return articlesList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ArticleDetails>> GetArticlesFromPolygon(string ticker)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.polygon.io/v2/reference/news?ticker={ticker}&order=desc&limit=5&sort=published_utc&apiKey={key}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var articlesFromPolygon = JObject.Parse(content).SelectToken("results").ToObject<IEnumerable<ArticleDetails>>();

                return articlesFromPolygon;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
