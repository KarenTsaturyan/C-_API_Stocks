using Microsoft.AspNetCore.Identity;

namespace StockApi.Models
{
    public class AppUser : IdentityUser
    {
        //for many to many
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
