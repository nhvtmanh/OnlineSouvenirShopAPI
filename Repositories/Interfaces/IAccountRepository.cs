using OnlineSouvenirShopAPI.DTOs.UserDTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AppUser>> GetAll();
        Task<IEnumerable<AppUser>> SearchUsers(UserQueryObject query);
    }
}
