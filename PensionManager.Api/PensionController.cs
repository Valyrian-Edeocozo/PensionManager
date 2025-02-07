using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PensionManager.PensionManager.Application.Helper;
using PensionManager.PensionManager.Application.Interfaces;
using PensionManager.PensionManager.Infrastructure;
using PensionManager.PensionManger.Domain;
using PensionManager.PensionManger.Domain.Dtos;
using PensionManager.PensionManger.Domain.Enums;

namespace PensionManager.PensionManager.Api;

[Route("api/v1/contribution")]
[ApiController]
public class PensionController(IContributionService contributionService) : ApiControllerBase
{
    private readonly IContributionService contributionService = contributionService;

    [HttpPost("contribute")]
    public async Task<IActionResult> Contribute([FromBody] ContributeRequestDto request)
    {
        var contributionResponse = await contributionService.Contribute(request);

        if(contributionResponse.message.Contains("failed"))
        {
            return BadRequest(contributionResponse);
        }
        return Ok(contributionResponse);
       
    }

}
