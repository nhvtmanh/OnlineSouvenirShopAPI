using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
