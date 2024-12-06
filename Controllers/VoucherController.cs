using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Interfaces;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;

        public VoucherController(IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vouchers = await _voucherRepository.GetAll();
            return Ok(vouchers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var voucher = await _voucherRepository.GetOne(id);
            if (voucher == null)
            {
                return NotFound(new { message = "Voucher not found" });
            }
            return Ok(voucher);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoucherDTO voucherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var voucher = _mapper.Map<Voucher>(voucherDTO);
            await _voucherRepository.Create(voucher);
            return Ok(voucher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, VoucherDTO voucherDTO)
        {
            var voucher = await _voucherRepository.GetOne(id);
            if (voucher == null)
            {
                return NotFound(new { message = "Voucher not found" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(voucherDTO, voucher);
            await _voucherRepository.Update(voucher);
            return Ok(voucher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var voucher = await _voucherRepository.Delete(id);
                return Ok(voucher);
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }
    }
}
