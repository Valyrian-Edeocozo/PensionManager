using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class GetUsersResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<UserDto>? Users { get; set; }
}

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public Guid EmployerId { get; set; }
    public Guid PensionPlanId { get; set; }
    public PensionPlanDto? PensionPlanDetails { get; set; } // Include pension details
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}

public class PensionPlanDto
{
    public Guid PensionPlanId { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
