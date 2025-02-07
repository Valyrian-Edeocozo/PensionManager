using Microsoft.AspNetCore.Identity;
using PensionManager.PensionManager.Application.Helper;
using PensionManager.PensionManager.Application.Interfaces;
using PensionManager.PensionManger.Domain;
using PensionManager.PensionManger.Domain.Dtos;

namespace PensionManager.PensionManager.Application
{
    public class AuthService(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly SignInManager<User> _signInManager = signInManager;

        public async Task<RegisterResponse> RegisterUser(RegisterDto model)
        {
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
                //EmployerId = model.EmployerId,
                //PensionPlanId = model.PensionPlanId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                Employer = new Employer
                {
                    //EmployerId = model.EmployerId,
                    Name = model.Employer.Name,
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                PensionPlan = new PensionPlan
                {
                    //PensionPlanId = model.PensionPlanId,
                    PlanName = "Life assurance",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    Details = "Life savings pension"
                }
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponse { Success = false, Message = "User creation failed.", Token = null };
            }

            var jwtHelper = new JwtHelper(_configuration);
            // Generate JWT token
            var token = jwtHelper.GenerateJwtToken(user);

            return new RegisterResponse { Success = true, Message = "User created successfully.", Token = token };
        }

        public async Task<LoginResponse> LogInUser(LoginRequest model)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new LoginResponse { Success = false, Message = "Invalid email or password." };
            }

            // Attempt to sign in the user
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new LoginResponse { Success = false, Message = "Invalid email or password." };
            }

            var jwtHelper = new JwtHelper(_configuration);
            // Generate JWT token
            var token = jwtHelper.GenerateJwtToken(user);

            return new LoginResponse { Success = true, Message = "Login successful.", Token = token };
        }
    }
}
