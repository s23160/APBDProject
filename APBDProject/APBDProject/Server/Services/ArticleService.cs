using APBDProject.Server.Data;
using APBDProject.Server.Models;
using APBDProject.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateArticleAsync(ArticlePost article)
        {
            await _context.Articles.AddAsync(new Article
            {
                IdentifierName = article.IdentifierName,
                Author = article.Author,
                Title = article.Title,
                Description = article.Description,
                PublishedDate = article.PublishedDate,
                ImageUrl = article.ImageUrl
            });
        }

        public IQueryable<Stock> GetStockByName(string ticker)
        {
            return _context.Stocks.Where(e => e.Ticker == ticker);
        }

        public async Task CreateStockArticleAsync(int idStock, int idArticle)
        {
            await _context.StockArticles.AddAsync(new StockArticle
            {
                IdStock = idStock,
                IdArticle = idArticle
            });
        }

        public IQueryable<Article> GetArticleByIndentifier(string identifierName)
        {
            return _context.Articles.Where(e => e.IdentifierName == identifierName);
        }

        public IQueryable<StockArticle> GetLastFiveArticles(string ticker)
        {
            return _context.StockArticles
                .Include(e => e.Stock)
                .Include(e => e.Article)
                .Where(e => e.Stock.Ticker == ticker)
                .OrderByDescending(e => e.Article.PublishedDate)
                .Take(5);
        }

        public async Task SaveChangesArticleAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> StockHasArticle(int idStock, int idArticle)
        {
            bool stockArticleExists = await _context.StockArticles.AnyAsync(e => (e.IdStock == idStock) && (e.IdArticle == idArticle));
            return stockArticleExists;
        }
    }
}
