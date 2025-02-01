using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PensionManager.PensionManager.Application.Helper;
using PensionManager.PensionManager.Infrastructure;
using PensionManager.PensionManger.Domain;
using PensionManager.PensionManger.Domain.Dtos;
using PensionManager.PensionManger.Domain.Enums;

namespace PensionManager.PensionManager.Api;

[Authorize()]
public class PensionController(ApplicationDbContext context) : ApiControllerBase
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Contribute([FromBody] ContributeRequestDto request)
    {
        if (request.ContributionType == ContributionType.Monthly)
        {
            var isWithinAllowedPeriod = (DateTime.UtcNow - _context.Contributions.Where(c => c.UserId == request.UserId).OrderByDescending(date => date.DateCreated).FirstOrDefault()!.DateCreated).TotalDays <= 30;

            if (isWithinAllowedPeriod)
            {
                // Process payment for monthly
                var contribution = new Contribution
                {
                    UserId = request.UserId,
                    ContributionType = request.ContributionType,
                    Amount = request.Amount,
                    ReferenceNumber = NumberGenerator.GenerateUniqueReference()
                };
                await _context.Contributions.AddAsync(contribution);

                await _context.SaveChangesAsync();

                return Ok(new ContributionResponse<ContributeRequestDto>
                {
                    message = $"Contribution succesful for user {request.UserId}",
                    data = new ContributeRequestDto { UserId = request.UserId, ContributionType = request.ContributionType, Amount = request.Amount }
                });
            }
            else
            {

                // return error message = "Monthly contribution can only be done once a month"
            }

        }
        else if (request.ContributionType == ContributionType.Voluntary)
        {
            // Process voluntary payment
            var contribution = new Contribution
            {
                UserId = request.UserId,
                ContributionType = request.ContributionType,
                Amount = request.Amount,
                ReferenceNumber = NumberGenerator.GenerateUniqueReference()
            };
            await _context.Contributions.AddAsync(contribution);

            await _context.SaveChangesAsync();

            return Ok(new ContributionResponse<ContributeRequestDto>
            {
                message = $"Contribution succesful for user {request.UserId}",
                data = new ContributeRequestDto { UserId = request.UserId, ContributionType = request.ContributionType, Amount = request.Amount }
            });
        }
        return Ok();
    }

}
