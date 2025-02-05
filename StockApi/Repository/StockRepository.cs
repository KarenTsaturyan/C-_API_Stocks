using Microsoft.EntityFrameworkCore;
using StockApi.Data;
using StockApi.Dtos.Stock;
using StockApi.Helpers;
using StockApi.Interfaces;
using StockApi.Models;

namespace StockApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _ctx;
        
        public StockRepository(ApplicationDbContext dbContext)//DI dbcontext
        {
            _ctx = dbContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _ctx.Stock.AddAsync(stockModel);
            await _ctx.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _ctx.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel is null)
            {
                return null;
            }
            //-dont have async remove-It does not involve any immediate database interaction when called, merely a state change in memory
            //-does not involve waiting and is a quick in-memory operation, making it asynchronous would not provide any benefits and could even lead to less efficient resource utilization.
            _ctx.Stock.Remove(stockModel);
            await _ctx.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            // include to see comments check stockDTO
            var stocks = _ctx.Stock.Include(c=>c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                // filtering with AsQueryable();
                stocks = stocks.Where(s=>s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            // sorting with AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            // Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            //.Find doesn't work with include
            return await _ctx.Stock.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _ctx.Stock.FirstOrDefaultAsync(i => i.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _ctx.Stock.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _ctx.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock is null)
            {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _ctx.SaveChangesAsync();

            return existingStock;
        }
    }
}
