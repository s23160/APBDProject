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
    [Route("api/prices")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IPriceService _service;
        public PricesController(IPriceService service)
        {
            _service = service;
        }

        [HttpPost("list")]
        public async Task<IActionResult> AddPrice(PriceListPost prices)
        {
            var stock = await _service.GetStockByName(prices.Prices[0].Ticker).FirstOrDefaultAsync();
            if(stock is null)
            {
                return Conflict();
            }
            try
            {

                foreach (var price in prices.Prices)
                {
                    if (await _service.GetPriceByDate(stock.IdStock, price.Day).FirstOrDefaultAsync() is null)
                    {
                        await _service.CreatePriceAsync(stock.IdStock, price);
                        await _service.SaveChangesPriceAsync();
                    }
                }
            }
            catch (Exception)
            {
                return Problem("Server error");
            }

            return NoContent();
        }

        [HttpPut("list/{ticker}")]
        public async Task<IActionResult> UpdatePrice(string ticker, PriceExtendPut pricePut)
        {
            var stock = await _service.GetStockByName(ticker).FirstOrDefaultAsync();
            if (stock is null)
            {
                return Conflict();
            }

            var price = await _service.GetPriceByDate(stock.IdStock, pricePut.Day).FirstOrDefaultAsync();
            if (price is null)
            {
                return Conflict();
            }

            if(price.PreMarket is not null && price.AfterHours is not null)
            {
                return Conflict();
            }

            try
            {
                await _service.UpdatePriceAsync(stock.IdStock, pricePut);
                await _service.SaveChangesPriceAsync();
            }
            catch (Exception)
            {
                return Problem("Server error");
            }

            return NoContent();
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetPrices(string ticker)
        {
            var stock = await _service.GetStockByName(ticker).FirstOrDefaultAsync();
            if(stock is null)
            {
                return Conflict();
            }
            return Ok(
                await _service.GetLastNinetyPrices(stock.IdStock)
                .Select(e =>
                new PriceGet
                {
                    Day = e.Day,
                    Open = e.Open,
                    High = e.High,
                    Low = e.Low,
                    Close = e.Close,
                    Volume = e.Volume,
                    AfterHours = e.AfterHours,
                    PreMarket = e.PreMarket
                }).ToListAsync()
                );
        }

        [HttpGet("list/{ticker}/{day}")]
        public async Task<IActionResult> GetPriceByDate(string ticker, DateTime day)
        {
            var stock = await _service.GetStockByName(ticker).FirstOrDefaultAsync();
            if (stock is null)
            {
                return Conflict();
            }

            var price = await _service.GetPriceByDate(stock.IdStock, day).FirstOrDefaultAsync();
            if (price is null)
            {
                return Conflict();
            }

            return Ok(new PriceGet
            {
                Day = price.Day,
                Open = price.Open,
                High = price.High,
                Low = price.Low,
                Close = price.Close,
                Volume = price.Volume,
                AfterHours = price.AfterHours,
                PreMarket = price.PreMarket
            });
        }

    }
}
