using System;

namespace PensionManager.PensionManger.Domain;

public class Employer : BaseEntity
{
    public string EmployerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Address Address { get; set; } = new Address();
}
