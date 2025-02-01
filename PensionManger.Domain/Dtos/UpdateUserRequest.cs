using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class UpdateUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirth { get; set; }
    public Address Address { get; set; }
    public string EmployerId { get; set; }
    public string PensionPlanId { get; set; }
}
