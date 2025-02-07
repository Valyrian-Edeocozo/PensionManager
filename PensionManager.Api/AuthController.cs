using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PensionManager.PensionManager.Application.Interfaces;
using PensionManager.PensionManger.Domain;
using PensionManager.PensionManger.Domain.Dtos;

namespace PensionManager.PensionManager.Api;

[Route("api/v1/user")]
[ApiController]
public class UserController(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager, IValidator<RegisterDto> validator, IAuthService authService) : ApiControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IValidator<RegisterDto> _validator = validator;
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var validationResult = await _validator.ValidateAsync(model);

        if(!validationResult.IsValid)
        {
            return BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
        }

        var authServiceResponse = await _authService.RegisterUser(model);

        if(authServiceResponse.Token != null)
        {
            return Ok(authServiceResponse);
        }
        return BadRequest(authServiceResponse);
    }
    
    [HttpPost("login")]
     public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var userLoginResponse = await _authService.LogInUser(model);
        if(string.IsNullOrEmpty(userLoginResponse.Token))
        {
            return Unauthorized(userLoginResponse);
        }
        return Ok(userLoginResponse);
    }
}
