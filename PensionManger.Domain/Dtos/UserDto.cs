using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class GetUsersResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<UserDto> Users { get; set; }
}

public class UserDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DateOfBirth { get; set; }
    public string NationalId { get; set; }
    public Address Address { get; set; }
    public string EmployerId { get; set; }
    public string PensionPlanId { get; set; }
    public PensionPlanDto PensionPlanDetails { get; set; } // Include pension details
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}

public class PensionPlanDto
{
    public string PensionPlanId { get; set; }
    public string PlanName { get; set; }
    public string Details { get; set; }
}
