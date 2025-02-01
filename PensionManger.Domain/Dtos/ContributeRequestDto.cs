using System;
using PensionManager.PensionManger.Domain.Enums;

namespace PensionManager.PensionManger.Domain.Dtos;

public class ContributeRequestDto
{
    public string UserId { get; set; }
    public ContributionType ContributionType { get; set; }
    public decimal Amount { get; set; }
}
