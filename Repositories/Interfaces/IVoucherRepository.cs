using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetAll();
        Task<Voucher?> GetOne(Guid id);
        Task<IEnumerable<Voucher>> GetByName(string name);
        Task Create(Voucher voucher);
        Task<Voucher> Update(Voucher voucher);
        Task<Voucher?> Delete(Guid id);
        Task<IEnumerable<Voucher>> FilterVouchers(byte status);
    }
}
