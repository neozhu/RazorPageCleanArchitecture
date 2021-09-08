using FluentValidation;

namespace CleanArchitecture.Razor.Application.Projects.Commands.AcceptChanges
{
    public class AcceptChangesProductCommandValidator : AbstractValidator<AcceptChangesProjectCommand>
    {
        public AcceptChangesProductCommandValidator()
        {
            RuleFor(v => v.Items)
                  .NotNull()
                  .NotEmpty();
             
        }
    }
}
