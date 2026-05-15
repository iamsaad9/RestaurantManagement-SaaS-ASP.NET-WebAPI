using FluentValidation;

public class CreateReservationValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.CustomerName)
        .NotEmpty()
        .WithMessage("Customer name is required")
        .MaximumLength(100);

        RuleFor(x => x.TableId)
        .GreaterThan(0);

        RuleFor(x => x.StartTime)
        .GreaterThan(DateTime.UtcNow)
        .WithMessage("Start time must be in the future");

        RuleFor(x => x.EndTime)
        .GreaterThan(x => x.StartTime)
        .WithMessage("End time must be after start time");
    }
}