using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
}
