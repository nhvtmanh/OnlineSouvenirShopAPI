using Microsoft.EntityFrameworkCore;
using OnlineSouvenirShopAPI.Data;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Interfaces;

namespace OnlineSouvenirShopAPI.Repositories.Implementations
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VoucherRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Voucher>> GetAll()
        {
            return await _dbContext.Vouchers.ToListAsync();
        }

        public async Task<Voucher?> GetOne(Guid id)
        {
            return await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Voucher voucher)
        {
            _dbContext.Vouchers.Add(voucher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Voucher> Update(Voucher voucher)
        {
            _dbContext.Vouchers.Update(voucher);
            await _dbContext.SaveChangesAsync();
            return voucher;
        }

        public async Task<Voucher?> Delete(Guid id)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
            if (voucher == null)
            {
                return null;
            }
            _dbContext.Vouchers.Remove(voucher);
            await _dbContext.SaveChangesAsync();
            return voucher;
        }

        public async Task<IEnumerable<Voucher>> GetByName(string name)
        {
            return await _dbContext.Vouchers
                .Where(x => x.Code.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Voucher>> FilterVouchers(byte status)
        {
            return await _dbContext.Vouchers.Where(x => x.Status == status).ToListAsync();
        }
    }
}
