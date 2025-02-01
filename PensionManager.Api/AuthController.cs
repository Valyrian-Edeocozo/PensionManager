using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PensionManager.PensionManger.Domain;
using PensionManager.PensionManger.Domain.Dtos;

namespace PensionManager.PensionManager.Api;


public class UserController(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager) : ApiControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly SignInManager<User> _signInManager = signInManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        // Check if passwords match
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest(new RegisterResponse { Success = false, Message = "Passwords do not match." });
        }

        // Create a new user
        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email,
            DateOfBirth = model.DateOfBirth,
            NationalId = model.NationalId,
            Address = model.Address,
            EmployerId = model.EmployerId,
            PensionPlanId = model.PensionPlanId,
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
            IsDeleted = false
        };

        // Attempt to create the user
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new RegisterResponse { Success = false, Message = "User creation failed.", Token = null });
        }

        // Generate JWT token
        var token = GenerateJwtToken(user);

        return Ok(new RegisterResponse { Success = true, Message = "User created successfully.", Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

        // await Task.CompletedTask;
        // // Call the service implemented for creating user
        // return Ok();
    
    [HttpPost("login")]
     public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        // Find the user by email
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return Unauthorized(new LoginResponse { Success = false, Message = "Invalid email or password." });
        }

        // Attempt to sign in the user
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Unauthorized(new LoginResponse { Success = false, Message = "Invalid email or password." });
        }

        // Generate JWT token
        var token = GenerateJwtToken(user);

        return Ok(new LoginResponse { Success = true, Message = "Login successful.", Token = token });
    }
}
