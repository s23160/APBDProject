using APBDProject.Server.Models;
using APBDProject.Shared.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public interface IArticleService
    {
        public IQueryable<StockArticle> GetLastFiveArticles(string ticker);
        public IQueryable<Article> GetArticleByIndentifier(string identifierName);
        public Task<bool> StockHasArticle(int idStock, int idArticle);
        public IQueryable<Stock> GetStockByName(string ticker);
        public Task CreateArticleAsync(ArticlePost article);
        public Task CreateStockArticleAsync(int idStock, int idArticle);
        public Task SaveChangesArticleAsync();
    }
}
