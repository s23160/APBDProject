using APBDProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public interface ISearchService
    {
        public Task<IEnumerable<StockMarket>> GetStockMarketsByName(string name);
    }
}
