using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Helpers.Enums;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Interfaces;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var vouchers = await _voucherRepository.GetByName(name);
            return Ok(vouchers);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterVouchers([FromQuery] byte status)
        {
            var vouchers = await _voucherRepository.FilterVouchers(status);
            return Ok(vouchers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoucherDTO voucherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var voucher = _mapper.Map<Voucher>(voucherDTO);
            voucher.Status = (byte)VoucherStatus.Inactive;
            await _voucherRepository.Create(voucher);
            return Ok(voucher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] VoucherDTO voucherDTO)
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
            var voucher = await _voucherRepository.Delete(id);
            if (voucher == null)
            {
                return NotFound(new { message = "Voucher not found" });
            }
            return Ok(voucher);
        }
    }
}
