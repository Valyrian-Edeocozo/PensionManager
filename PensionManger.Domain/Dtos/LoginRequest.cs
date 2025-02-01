using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
