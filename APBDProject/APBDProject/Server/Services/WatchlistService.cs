using APBDProject.Server.Data;
using APBDProject.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly ApplicationDbContext _context;

        public WatchlistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateWatchlistAsync(string idUser, int idStock)
        {
            await _context.ApplicationUserStocks.AddAsync(new ApplicationUserStock { Id = idUser, IdStock = idStock });
        }

        public async Task DeleteWatchlistAsync(string idUser, int idStock)
        {
            _context.ApplicationUserStocks.Remove(await _context.ApplicationUserStocks.Where(e => (e.Id == idUser) && (e.IdStock==idStock)).FirstOrDefaultAsync());
        }

        public IQueryable<ApplicationUserStock> GetAllStocksFromWatchlist(string idUser)
        {
            return _context.ApplicationUserStocks
                .Include(e => e.Stock)
                .Include(e => e.ApplicationUser)
                .Where(e => e.Id == idUser);
        }

        public IQueryable<Stock> GetStockByName(string ticker)
        {
            return _context.Stocks.Where(e => e.Ticker == ticker);
        }

        public IQueryable<ApplicationUserStock> GetStockFromWatchlistById(string idUser, int idStock)
        {
            return _context.ApplicationUserStocks.Where(e => (e.Id == idUser) && (e.IdStock == idStock));
        }

        public IQueryable<ApplicationUser> GetUserByMail(string mail)
        {
            return _context.Users.Where(e => e.Email == mail);
        }

        public async Task SaveChangesWatchlistAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> StocksExistsInWatchlist(string idUser)
        {
            bool stocksExists = await _context.ApplicationUserStocks.AnyAsync(e => e.Id == idUser);
            return stocksExists;
        }
    }
}
