using FluentValidation;
using System;

namespace PensionManager.PensionManger.Domain.Dtos;

public class RegisterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string? Address { get; set; }
    //public Guid EmployerId { get; set; }
    //public Guid PensionPlanId { get; set; }
    public Employer? Employer { get; set; }
    //public Guid PensionPlanId { get; set; }
    //public PensionPlan? PensionPlan { get; set; }
}

public class RegisterDtoValidation : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidation()
    {
        RuleFor(s => s.FirstName).NotEmpty().NotNull().WithMessage("First name cannot be null or empty");
        RuleFor(s => s.LastName).NotEmpty().NotNull().WithMessage("First name cannot be null or empty");
        RuleFor(s => s.Email).NotEmpty().NotNull().WithMessage("Email cannot be null or empty");
        RuleFor(s => s.Password).NotEmpty().NotNull().WithMessage("Password cannot be null or empty");
        RuleFor(s => s.ConfirmPassword).NotEmpty().NotNull().WithMessage("ConfirmPassword cannot be null or empty");
        RuleFor(s => s.DateOfBirth)
            .Cascade(CascadeMode.Stop) // Stop validation on first failure
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAValidDate).WithMessage("Invalid date format.")
            .Must(BeWithinAgeRange).WithMessage("Age must be between 18 and 70 years.");

        RuleFor(s => s.NationalId).NotEmpty().NotNull().WithMessage("NationalId cannot be null or empty");
        RuleFor(s => s.Address).NotEmpty().NotNull().WithMessage("Address name cannot be null or empty");
        RuleFor(s => s.Employer.Name).NotEmpty().NotNull().WithMessage("Employer name cannot be null or empty");
        RuleFor(s => s.Employer.EmployerId).NotEmpty().NotNull().WithMessage("EmployerId name cannot be null or empty");
        RuleFor(s => s.Password).Equal(a => a.ConfirmPassword).WithMessage("Password must match");

    }

    // Check if the date string is valid
    private bool BeAValidDate(string dateString)
    {
        return DateTime.TryParse(dateString, out _);
    }

    // Check if the age is between 18 and 70
    private bool BeWithinAgeRange(string dateString)
    {
        if (!DateTime.TryParse(dateString, out DateTime dateOfBirth))
            return false;

        var today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;

        // Adjust age if the birthday hasn't occurred yet this year
        if (dateOfBirth.Date > today.AddYears(-age))
            age--;

        return age >= 18 && age <= 70;
    }
}