using System;

namespace PensionManager.PensionManger.Domain;

public class BaseEntity
{
    public bool IsDeleted { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set;}
}
