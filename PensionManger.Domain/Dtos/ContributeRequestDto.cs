using System;
using FluentValidation;
using PensionManager.PensionManger.Domain.Enums;

namespace PensionManager.PensionManger.Domain.Dtos;

public class ContributeRequestDto
{
    public string UserId { get; set; }
    public ContributionType ContributionType { get; set; }
    public decimal Amount { get; set; }
}

public class ContributeRequestDtoValidation : AbstractValidator<ContributeRequestDto>
{
    public ContributeRequestDtoValidation()
    {
        RuleFor(s => s.UserId).NotEmpty().NotNull().WithMessage("UserId cannot be null or empty");
        RuleFor(s => s.ContributionType)
            .IsInEnum()
            .WithMessage("Contribution type must be either Monthly or Voluntary.");

        RuleFor(s => s.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");
    }
}
