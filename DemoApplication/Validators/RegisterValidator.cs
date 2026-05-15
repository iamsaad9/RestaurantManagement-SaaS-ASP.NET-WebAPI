using FluentValidation;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(l => l.Name)
        .NotEmpty()
        .WithMessage("Email is required")
        .MaximumLength(100);

        RuleFor(l => l.Email)
        .EmailAddress()
        .NotEmpty()
        .WithMessage("Email is required");

        RuleFor(l => l.Password)
        .MinimumLength(8)
        .MaximumLength(20);
    }
}