using APBDProject.Server.Models;
using APBDProject.Server.Services;
using APBDProject.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Controllers
{
    [Authorize]
    [Route("api/watchlists")]
    [ApiController]
    public class WatchlistsController : ControllerBase
    {
        private readonly IWatchlistService _service;
        public WatchlistsController(IWatchlistService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddStockToWatchlist(UserWatchlist userStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong data");
            }

            var userFromDatabase = await _service.GetUserByMail(userStock.IdUser).FirstOrDefaultAsync();
            var watchlistStock = await _service.GetStockByName(userStock.Ticker).FirstOrDefaultAsync();
            if(watchlistStock is null)
            {
                return Conflict("Stock does not exist in database");
            }

            if(await _service.GetStockFromWatchlistById(userFromDatabase.Id, watchlistStock.IdStock).FirstOrDefaultAsync() is not null)
            {
                return Conflict("Stock exsists in watchlist");
            }

            try
            {
                await _service.CreateWatchlistAsync(userFromDatabase.Id, watchlistStock.IdStock);
                await _service.SaveChangesWatchlistAsync();

            }
            catch (Exception)
            {
                return Problem("Server error");
            }

            return NoContent();
        }

        [HttpDelete("{ticker}/{idUser}")]
        public async Task<IActionResult> RemoveStockFromWatchlist(string ticker, string idUser)
        {
            var userFromDatabase = await _service.GetUserByMail(idUser).FirstOrDefaultAsync();
            var watchlistStock = await _service.GetStockByName(ticker).FirstOrDefaultAsync();
            if (watchlistStock is null)
            {
                return Conflict("Stock does not exist in database");
            }

            if (await _service.GetStockFromWatchlistById(userFromDatabase.Id, watchlistStock.IdStock).FirstOrDefaultAsync() is null)
            {
                return Conflict("Stock does not exist in watchlist");
            }

            try
            {
                await _service.DeleteWatchlistAsync(userFromDatabase.Id, watchlistStock.IdStock);
                await _service.SaveChangesWatchlistAsync();
            }
            catch (Exception)
            {
                return Problem("Server error");
            }

            return NoContent();
        }

        [HttpGet("{user}")]
        public async Task<IActionResult> GetStockFromWatchlist(string user)
        {
            var userFromDatabase = await _service.GetUserByMail(user).FirstOrDefaultAsync();
            bool stockExistsInWatchlist = await _service.StocksExistsInWatchlist(userFromDatabase.Id);
            if(!stockExistsInWatchlist)
            {
                return NotFound("There is nothing in watchlist");
            }

            return Ok(await _service.GetAllStocksFromWatchlist(userFromDatabase.Id).Select(e => new UserWatchlistGet
                {
                    Ticker = e.Stock.Ticker,
                    Name = e.Stock.Name,
                    Market = e.Stock.Market,
                    Locale = e.Stock.Locale,
                    PrimaryExchange = e.Stock.PrimaryExchange,
                    Type = e.Stock.Type,
                    CurrencyName = e.Stock.CurrencyName,
                    SicDescription = e.Stock.SicDescription,
                    City = e.Stock.City,
                    PhoneNumber = e.Stock.PhoneNumber,
                    Description = e.Stock.Description,
                    Logo = e.Stock.Logo,
                    Homepage = e.Stock.Homepage
                }).ToListAsync());
        }
    }
}
