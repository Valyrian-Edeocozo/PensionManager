using Microsoft.AspNetCore.Mvc;
using PensionManager.PensionManger.Domain.Dtos;

namespace PensionManager.PensionManager.Application.Interfaces
{
    public interface IContributionService
    {
        Task<ContributionResponse<ContributeRequestDto>> Contribute(ContributeRequestDto request);
    }
}
