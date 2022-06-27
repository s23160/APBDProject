using APBDProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public interface IStockDetailsService
    {
        public Task<StockMarketDetails> GetStockFromPolygon(string name);
        public Task<StockMarketDetails> GetStockFromDatabase(string name);
        public Task<string> AddStockToDatabase(StockMarketDetails stock);
        public Task<string> UpdateStockInDatabase(StockMarketDetails stock);
    }
}
