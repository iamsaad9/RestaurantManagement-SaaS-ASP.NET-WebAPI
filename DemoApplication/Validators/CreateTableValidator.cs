using System.Data;
using FluentValidation;

public class CreateTableValidator : AbstractValidator<CreateTableDto>
{
    public CreateTableValidator()
    {
        RuleFor(t => t.TableNumber)
        .GreaterThan(0);

        RuleFor(t => t.Capacity)
        .GreaterThan(0);
    }
}