using System;

namespace PensionManager.PensionManger.Domain;

public class PensionPlan : BaseEntity
{
    public Guid PensionPlanId { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = new List<User>();
}
