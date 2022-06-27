using APBDProject.Server.Models;
using APBDProject.Shared.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Services
{
    public interface IPriceService
    {
        public IQueryable<Price> GetLastNinetyPrices(int idStock);
        public IQueryable<Price> GetPriceByDate(int idStock, DateTime date);
        public Task CreatePriceAsync(int idStock, PricePost price);
        public Task SaveChangesPriceAsync();
        public IQueryable<Stock> GetStockByName(string ticker);
        public Task UpdatePriceAsync(int idStock, PriceExtendPut pricePut);
    }
}
