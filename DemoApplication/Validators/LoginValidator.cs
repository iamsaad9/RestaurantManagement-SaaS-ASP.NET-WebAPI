using FluentValidation;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email)
        .EmailAddress()
        .NotEmpty()
        .WithMessage("Email is required");

        RuleFor(l => l.Password)
        .MinimumLength(8)
        .MaximumLength(20);
    }
}