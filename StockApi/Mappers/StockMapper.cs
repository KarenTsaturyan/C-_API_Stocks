using StockApi.Dtos.Stock;
using StockApi.Models;

namespace StockApi.Mappers
{
    public static class StockMapper
    {
        public static StockDto toStockDto(this Stock stockModel)
        {
            // in Get we return DTO model that's why we take Stock as parameter
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock toStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            // in Post we return actual Model
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }

    }
}
