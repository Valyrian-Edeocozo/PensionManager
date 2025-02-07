using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace PensionManager.PensionManger.Domain;

public class User : IdentityUser
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string? Address { get; set; }
    public decimal AvailableBalance { get; set; }
    public Employer? Employer { get; set; } 
    public Guid EmployerId { get; set; }
    public PensionPlan? PensionPlan { get; set; }
    public Guid PensionPlanId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set;}
}
