using APBDProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public interface IWatchlistDetailsService
    {
        public Task<IEnumerable<WatchlistDetails>> GetWatchlist(string user);
        public Task<string> AddStockToWatchlist(string name, string idUser);
        public Task<string> RemoveStockFromWatchlist(string ticker, string idUser);
    }
}
