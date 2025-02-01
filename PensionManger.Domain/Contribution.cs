using System;
using PensionManager.PensionManger.Domain.Enums;

namespace PensionManager.PensionManger.Domain;


public class Contribution : BaseEntity
{

    public int Id { get; set; }

    // public int MemberId { get; set; }

    public ContributionType ContributionType { get; set; }

    public decimal Amount { get; set; }
    public string ReferenceNumber { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    // Navigation property

    // public Member Member { get; set; }
}
