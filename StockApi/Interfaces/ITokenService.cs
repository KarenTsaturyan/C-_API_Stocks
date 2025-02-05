using StockApi.Models;

namespace StockApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
