using FluentValidation;
using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}


public class LoginRequestValidation : AbstractValidator<LoginRequest>
{
    public LoginRequestValidation()
    {
        RuleFor(s => s.Email).NotEmpty().NotNull().WithMessage("Email cannot be null or empty");
        RuleFor(s => s.Password).NotEmpty().NotNull().WithMessage("Password cannot be null or empty");
    }
}