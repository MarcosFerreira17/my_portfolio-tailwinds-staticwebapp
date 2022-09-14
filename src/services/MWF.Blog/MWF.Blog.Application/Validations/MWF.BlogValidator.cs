using FluentValidation;
using MWF.Blog.Domain.Entities;

namespace MWF.Blog.Application.Validations;

public class MWF.BlogValidator : AbstractValidator<MWF.BlogEntity>
{
    public MWF.BlogValidator()
    {
        RuleFor(x => x.Id == 0)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id field must be filled.");
    }
}
