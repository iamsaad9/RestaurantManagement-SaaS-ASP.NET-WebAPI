using FluentValidation;

public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantValidator()
    {
        RuleFor(r => r.Name)
        .NotEmpty()
        .WithMessage("Restaurant Name is required")
        .MaximumLength(100);
    }
}