using APBDProject.Server.Data;
using APBDProject.Server.Models;
using APBDProject.Shared.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;

        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateStockAsync(StockPost stock)
        {
            await _context.Stocks.AddAsync(new Stock
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

        public IQueryable<Stock> GetStockByName(string ticker)
        {
            return _context.Stocks.Where(e => e.Ticker == ticker);
        }

        public async Task SaveChangesStockAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(int idStock, StockPut stock)
        {
            var stockFromDatabase = await _context.Stocks.FindAsync(idStock);
            stockFromDatabase.Ticker = stock.Ticker;
            stockFromDatabase.Name = stock.Name;
            stockFromDatabase.Market = stock.Market;
            stockFromDatabase.Locale = stock.Locale;
            stockFromDatabase.PrimaryExchange = stock.PrimaryExchange;
            stockFromDatabase.Type = stock.Type;
            stockFromDatabase.CurrencyName = stock.CurrencyName;
            stockFromDatabase.SicDescription = stock.SicDescription;
            stockFromDatabase.City = stock.City;
            stockFromDatabase.PhoneNumber = stock.PhoneNumber;
            stockFromDatabase.Description = stock.Description;
            stockFromDatabase.Logo = stock.Logo;
            stockFromDatabase.Homepage = stock.Homepage;
        }
    }
}
