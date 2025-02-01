using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace PensionManager.PensionManger.Domain;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string DateOfBirth { get; set; }
    public string NationalId { get; set; }
    public Address Address { get; set; }
    public Employer Employer { get; set; }
    public string EmployerId { get; set; }
    public PensionPlan PensionPlan { get; set; }
    public string PensionPlanId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set;}
}
