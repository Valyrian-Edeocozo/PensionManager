using System;

namespace PensionManager.PensionManger.Domain;

public class Employer : BaseEntity
{
    public Guid EmployerId { get; set; }
    public string Name { get; set; } = string.Empty;
    //public Address Address { get; set; } = new Address();
}
