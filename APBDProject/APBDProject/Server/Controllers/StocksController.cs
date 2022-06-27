using APBDProject.Server.Services;
using APBDProject.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Controllers
{
    [Authorize]
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _service;
        public StocksController(IStockService service)
        {
            _service = service;
        }

        [HttpPost("list")]
        public async Task<IActionResult> AddStock(StockPost stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong data");
            }

            if(await _service.GetStockByName(stock.Ticker).FirstOrDefaultAsync() is not null)
            {
                return Conflict("Stock has already been created");
            }

            try
            {
                await _service.CreateStockAsync(stock);
                await _service.SaveChangesStockAsync();

            }
            catch (Exception)
            {
                return Problem("Server error");
            }

            return NoContent();
        }

        [HttpPut("list/{ticker}")]
        public async Task<IActionResult> UpdateStock(string ticker, StockPut stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong data");
            }

            if (await _service.GetStockByName(stock.Ticker).FirstOrDefaultAsync() is null)
            {
                return Conflict("Stock has not been created");
            }

            var stockFromDatabase = await _service.GetStockByName(ticker).FirstOrDefaultAsync();

            try
            {
                await _service.UpdateStockAsync(stockFromDatabase.IdStock, stock);
                await _service.SaveChangesStockAsync();

            }
            catch (Exception)
            {
                return Problem("Server error");
            }

            return NoContent();
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetStock(string ticker)
        {
            var stock = await _service.GetStockByName(ticker).FirstOrDefaultAsync();
            if (stock is null)
            {
                return Conflict("Stock has not been created");
            }

            return Ok(new StockGet
            {
                Ticker = stock.Ticker,
                Name = stock.Name,
                Market = stock.Market,
                Locale = stock.Locale,
                PrimaryExchange = stock.PrimaryExchange,
                Type = stock.Type,
                CurrencyName = stock.CurrencyName,
                SicDescription = stock.SicDescription,
                City = stock.City,
                PhoneNumber = stock.PhoneNumber,
                Description = stock.Description,
                Logo = stock.Logo,
                Homepage = stock.Homepage
            });
        }
    }
}
