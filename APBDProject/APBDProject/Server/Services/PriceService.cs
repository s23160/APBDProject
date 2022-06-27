using APBDProject.Server.Data;
using APBDProject.Server.Models;
using APBDProject.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public class PriceService : IPriceService
    {
        private readonly ApplicationDbContext _context;

        public PriceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Stock> GetStockByName(string ticker)
        {
            return _context.Stocks.Where(e => e.Ticker == ticker);
        }
        public async Task CreatePriceAsync(int idStock, PricePost price)
        {
            await _context.Prices.AddAsync(new Price
            {
                IdStock = idStock,
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

        public IQueryable<Price> GetLastNinetyPrices(int idStock)
        {
            return _context.Prices
                .Where(e => e.IdStock==idStock).OrderByDescending(e => e.Day).Take(90);
        }

        public IQueryable<Price> GetPriceByDate(int idStock, DateTime date)
        {
            return _context.Prices.Where(e => (e.Day == date) && (e.IdStock == idStock));
        }

        public async Task SaveChangesPriceAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePriceAsync(int idStock, PriceExtendPut pricePut)
        {
            var price = await _context.Prices.Where(e => (e.Day == pricePut.Day) && (e.IdStock == idStock)).FirstOrDefaultAsync();
            price.AfterHours = pricePut.AfterHours;
            price.PreMarket = pricePut.PreMarket;
        }
    }
}
