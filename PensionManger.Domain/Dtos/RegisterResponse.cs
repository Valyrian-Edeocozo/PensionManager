using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class RegisterResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
}
