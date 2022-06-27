using APBDProject.Server.Models;
using APBDProject.Shared.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public interface IStockService
    {
        public IQueryable<Stock> GetStockByName(string ticker);
        public Task CreateStockAsync(StockPost stock);
        public Task UpdateStockAsync(int idStock, StockPut stock);
        public Task SaveChangesStockAsync();
    }
}
