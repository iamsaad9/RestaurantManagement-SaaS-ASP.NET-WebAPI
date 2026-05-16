using System.Data;
using FluentValidation;

public class UpdateTableValidator : AbstractValidator<UpdateTableDto>
{
    public UpdateTableValidator()
    {
        RuleFor(t => t.TableNumber)
        .GreaterThan(0);

        RuleFor(t => t.Capacity)
        .GreaterThan(0);
    }
}