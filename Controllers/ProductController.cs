using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.DTOs.ProductDTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Implementations;
using System.Security.Claims;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingQueryObject queryObject)
        {
            var products = await _productRepository.GetAll(queryObject);
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

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var products = await _productRepository.GetByName(name);
            return Ok(products);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByCategory([FromQuery] string name)
        {
            var products = await _productRepository.GetByCategory(name);
            return Ok(products);
        }

        [HttpGet("get-favorite")]
        public async Task<IActionResult> GetFavorite()
        {
            var customerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var favoriteProducts = await _productRepository.GetFavorite(customerId);
            return Ok(favoriteProducts);
        }

        [HttpPost("add-favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] Guid productId)
        {
            var product = await _productRepository.GetOne(productId);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            var customerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var favoriteProduct = await _productRepository.AddFavorite(customerId, productId);

            if (favoriteProduct == null)
            {
                return BadRequest(new { message = "Product already in favorites" });
            }

            return Ok(favoriteProduct);
        }

        [HttpDelete("remove-favorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] Guid productId)
        {
            var product = await _productRepository.GetOne(productId);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            var customerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var favoriteProduct = await _productRepository.RemoveFavorite(customerId, productId);

            if (favoriteProduct == null)
            {
                return BadRequest(new { message = "Product not in favorites" });
            }

            return Ok(favoriteProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDTO.File == null || productDTO.File.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            // Save file to local folder
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productDTO.File.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDTO.File.CopyToAsync(stream);
            }

            var product = _mapper.Map<Product>(productDTO);
            product.ImageUrl = $"uploads/{fileName}";

            await _productRepository.Create(product);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateProductDTO productDTO)
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

            if (productDTO.File == null || productDTO.File.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            // Save file to local folder
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productDTO.File.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productDTO.File.CopyToAsync(stream);
            }

            _mapper.Map(productDTO, product);
            product.ImageUrl = $"uploads/{fileName}";

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
