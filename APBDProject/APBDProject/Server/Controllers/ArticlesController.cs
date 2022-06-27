using APBDProject.Server.Services;
using APBDProject.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace APBDProject.Server.Controllers
{
    [Authorize]
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;
        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        [HttpPost("list")]
        public async Task<IActionResult> AddArticle(ArticleListPost articles)
        {


            foreach (var article in articles.Articles)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Wrong data");
                }

                var articleFromDatabase = await _service.GetArticleByIndentifier(article.IdentifierName).FirstOrDefaultAsync();
                bool addArticleToDatabase = false;
                if (articleFromDatabase is null)
                {
                    addArticleToDatabase = true;
                }

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var stock = await _service.GetStockByName(article.Ticker).FirstOrDefaultAsync();

                        if (addArticleToDatabase)
                        {
                            await _service.CreateArticleAsync(article);
                            await _service.SaveChangesArticleAsync();
                            articleFromDatabase = await _service.GetArticleByIndentifier(article.IdentifierName).FirstOrDefaultAsync();
                        }

                        bool stockHasArticle = await _service.StockHasArticle(stock.IdStock, articleFromDatabase.IdArticle);
                        if (!stockHasArticle)
                        {
                            await _service.CreateStockArticleAsync(stock.IdStock, articleFromDatabase.IdArticle);
                            await _service.SaveChangesArticleAsync();
                        }

                        scope.Complete();
                    }
                    catch (Exception)
                    {
                        return Problem("Server error");
                    }
                }
            }

            return NoContent();
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetArticles(string ticker)
        {
            var stock = await _service.GetStockByName(ticker).FirstOrDefaultAsync();
            if (stock is null)
            {
                return Conflict();
            }

            return Ok(
                await _service.GetLastFiveArticles(ticker)
                .Select(e =>
                new ArticleGet
                {
                    IdentifierName = e.Article.IdentifierName,
                    Author = e.Article.Author,
                    Title = e.Article.Title,
                    Description = e.Article.Description,
                    PublishedDate = e.Article.PublishedDate,
                    ImageUrl = e.Article.ImageUrl
                }).ToListAsync()
                );
        }


    }
}
