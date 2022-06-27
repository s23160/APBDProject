using APBDProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBDProject.Client.Services
{
    public interface IPriceDetailsService
    {
        public Task<IEnumerable<OHLCDetails>> GetPricesFromPolygon(string name);
        public Task<IEnumerable<OHLCDetails>> GetPricesFromDatabase(string name);
        public Task<PriceDetailsExtend> GetPriceExtendFromDatabase(string name, DateTime day);
        public Task<string> AddPricesToDatabase(List<OHLCDetails> ohlc);
        public Task<PriceDetailsExtend> GetPriceExtendFromPolygon(string name, DateTime day);
        public Task<string> AddPriceExtendToDatabase(PriceDetailsExtend price);
    }
}
