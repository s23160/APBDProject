using APBDProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public interface IArticleDetailsService
    {
        public Task<IEnumerable<ArticleDetails>> GetArticlesFromPolygon(string ticker);
        public Task<IEnumerable<ArticleDetails>> GetArticlesFromDatabase(string ticker);
        public Task<string> AddArticlesToDatabase(string ticker, IEnumerable<ArticleDetails> articleList);
    }
}
