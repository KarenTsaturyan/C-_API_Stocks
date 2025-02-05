using Microsoft.EntityFrameworkCore;
using StockApi.Data;
using StockApi.Interfaces;
using StockApi.Models;

namespace StockApi.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _ctx;
        public PortfolioRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _ctx.Portfolios.AddAsync(portfolio);
            await _ctx.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
              return await _ctx.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap,
                }).ToListAsync();
        }
    }
}
