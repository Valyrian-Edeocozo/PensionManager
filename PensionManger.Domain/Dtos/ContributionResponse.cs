using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class ContributionResponse<T>
{
    public T? data { get; set; }
    public string message { get; set; } = string.Empty;
}
