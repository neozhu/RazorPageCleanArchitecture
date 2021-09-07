using FluentValidation;

namespace CleanArchitecture.Razor.Application.Projects.Commands.AddEdit
{
    public class AddEditProjectCommandValidator : AbstractValidator<AddEditProjectCommand>
    {
        public AddEditProjectCommandValidator()
        {
            RuleFor(v => v.Name)
                  .MaximumLength(256)
                  .NotEmpty();
            RuleFor(v => v.BeginDateTime)
                 .NotNull();
        }
    }
}
