﻿using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetAll();
        Task<Voucher?> GetOne(Guid id);
        Task Create(Voucher voucher);
        Task<Voucher> Update(Voucher voucher);
        Task<Voucher?> Delete(Guid id);
    }
}
