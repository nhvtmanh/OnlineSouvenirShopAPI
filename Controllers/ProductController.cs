﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Implementations;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var product = await _productRepository.GetOne(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(product);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDTO productDTO)
        {
            var product = await _productRepository.GetOne(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(productDTO, product);
            await _productRepository.Update(product);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.Delete(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            return Ok(product);
        }
    }
}
