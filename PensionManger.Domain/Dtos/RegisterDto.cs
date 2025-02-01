using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string DateOfBirth { get; set; }
    public string NationalId { get; set; }
    public Address Address { get; set; }
    public string EmployerId { get; set; }
    public string PensionPlanId { get; set; }
}
