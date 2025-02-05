using StockApi.Dtos.Comment;
using StockApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApi.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        // To show comment of the stock
        public List<CommentDto> Comments { get; set; }
    }
}
