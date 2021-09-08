using FluentValidation;

namespace CleanArchitecture.Razor.Application.Projects.Commands.Create
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(v => v.Name)
                  .MaximumLength(256)
                  .NotEmpty();
            RuleFor(v => v.BeginDateTime)
                 .NotNull();
        }
    }
}
