using APBDProject.Server.Models;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public interface IWatchlistService
    {
        public IQueryable<ApplicationUserStock> GetStockFromWatchlistById(string idUser, int idStock);
        public IQueryable<ApplicationUserStock> GetAllStocksFromWatchlist(string idUser);
        public IQueryable<Stock> GetStockByName(string ticker);
        public IQueryable<ApplicationUser> GetUserByMail(string mail);
        public Task<bool> StocksExistsInWatchlist(string idUser);
        public Task CreateWatchlistAsync(string idUser, int idStock);
        public Task SaveChangesWatchlistAsync();
        public Task DeleteWatchlistAsync(string idUser, int idStock);
    }
}
