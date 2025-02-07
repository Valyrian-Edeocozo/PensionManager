using FluentValidation;
using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class UpdateUserRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string? Address { get; set; }
    public Guid EmployerId { get; set; }
    public Guid PensionPlanId { get; set; }
}