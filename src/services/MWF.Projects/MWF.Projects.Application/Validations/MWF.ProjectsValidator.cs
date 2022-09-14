using FluentValidation;
using MWF.Projects.Domain.Entities;

namespace MWF.Projects.Application.Validations;

public class MWF.ProjectsValidator : AbstractValidator<MWF.ProjectsEntity>
{
    public MWF.ProjectsValidator()
    {
        RuleFor(x => x.Id == 0)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id field must be filled.");
    }
}
