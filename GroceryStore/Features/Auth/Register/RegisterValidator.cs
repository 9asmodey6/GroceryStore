namespace GroceryStore.Features.Auth.Register;

using FluentValidation;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator(RegisterHandler handler)
    {
        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

        RuleFor(r => r.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid email format")
            .MustAsync(handler.IsEmailUniqueAsync);
    }
}