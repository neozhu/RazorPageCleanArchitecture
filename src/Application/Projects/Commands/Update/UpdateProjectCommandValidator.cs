using FluentValidation;

namespace CleanArchitecture.Razor.Application.Projects.Commands.Update
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {

            
            RuleFor(v => v.Name)
                 .MaximumLength(256)
                 .NotEmpty();
            RuleFor(v => v.BeginDateTime)
                 .NotNull();
        }
    }
}
