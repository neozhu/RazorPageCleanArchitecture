using FluentValidation;

namespace CleanArchitecture.Razor.Application.Projects.Commands.Import
{
    public class ImportProjectsCommandValidator : AbstractValidator<ImportProjectsCommand>
    {
        public ImportProjectsCommandValidator()
        {
            RuleFor(v => v.Data)
                 .NotNull()
                 .NotEmpty();
        }
    }
}
