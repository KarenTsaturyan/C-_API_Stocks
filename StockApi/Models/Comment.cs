﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StockApi.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        // one to many Navigate Stock.property
        public Stock? Stock { get; set; }
    }
}
