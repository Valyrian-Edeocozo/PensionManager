using PensionManager.PensionManger.Domain.Dtos;

namespace PensionManager.PensionManager.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterUser(RegisterDto model);
        Task<LoginResponse> LogInUser(LoginRequest model);
    }
}
