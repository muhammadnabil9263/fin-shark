using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;

    }





    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO userLogin)
    {

        var user = await _userManager.FindByNameAsync(userLogin.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password))
        {
            var token = _tokenService.CreateToken(user);
            return Ok(new NewUserDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = token
            });
        }

        return Unauthorized("wrong password or username");
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(
                        new NewUserDTO
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        }
                       );
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }


        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }

    }
}

