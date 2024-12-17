using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Services.Interfaces;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = _mapper.Map<AppUser>(registerDTO);
                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Customer");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDTO
                        {
                            UserName = appUser.UserName!,
                            Email = appUser.Email!,
                            Token = _tokenService.CreateToken(appUser)
                        });
                    }
                    else
                    {
                        return BadRequest(new { message = roleResult.Errors });
                    }
                }
                else
                {
                    return BadRequest(new { message = createdUser.Errors });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
