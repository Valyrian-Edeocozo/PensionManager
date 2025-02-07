using Microsoft.EntityFrameworkCore;
using PensionManager.PensionManager.Application.Helper;
using PensionManager.PensionManager.Application.Interfaces;
using PensionManager.PensionManager.Infrastructure;
using PensionManager.PensionManger.Domain;
using PensionManager.PensionManger.Domain.Dtos;
using PensionManager.PensionManger.Domain.Enums;

namespace PensionManager.PensionManager.Application
{
    public class ContributionService(ApplicationDbContext context) : IContributionService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ContributionResponse<ContributeRequestDto>> Contribute(ContributeRequestDto request)
        {
            if (request.ContributionType == ContributionType.Monthly)
            {
                //Check if user has existing contribution record in the system
                var userContribution = _context.Contributions.Where(s => s.UserId == request.UserId).FirstOrDefault();


                bool isWithinAllowedPeriod = false;
                if (userContribution is not null)
                {
                    isWithinAllowedPeriod = (DateTime.UtcNow - _context.Contributions.Where(c => c.UserId == request.UserId).OrderByDescending(date => date.DateCreated).FirstOrDefault()!.DateCreated).TotalDays <= 30;

                }


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
                    var user = await _context.Users.FirstOrDefaultAsync(s => s.Id == contribution.UserId);

                    user.AvailableBalance += request.Amount;
                    await _context.Contributions.AddAsync(contribution);

                    await _context.SaveChangesAsync();

                    return new ContributionResponse<ContributeRequestDto>
                    {
                        message = $"Contribution succesful for user {request.UserId}",
                        data = new ContributeRequestDto { UserId = request.UserId, ContributionType = request.ContributionType, Amount = request.Amount }
                    };
                }
                else
                {

                    // return error message = "Monthly contribution can only be done once a month"
                    return new ContributionResponse<ContributeRequestDto>
                    {
                        message = $"Contribution failed for user {request.UserId}",
                        data = new ContributeRequestDto { UserId = request.UserId, ContributionType = request.ContributionType, Amount = request.Amount }
                    };
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

                var user = await _context.Users.FirstOrDefaultAsync(s => s.Id == contribution.UserId);

                user.AvailableBalance += request.Amount;

                await _context.Contributions.AddAsync(contribution);

                await _context.SaveChangesAsync();

                return new ContributionResponse<ContributeRequestDto>
                {
                    message = $"Contribution succesful for user {request.UserId}",
                    data = new ContributeRequestDto { UserId = request.UserId, ContributionType = request.ContributionType, Amount = request.Amount }
                };
            }
            return new ContributionResponse<ContributeRequestDto>
            {
                message = $"Contribution failed for user {request.UserId}",
                data = new ContributeRequestDto { UserId = request.UserId, ContributionType = request.ContributionType, Amount = request.Amount }
            };
        }
    }
}
