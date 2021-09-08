using FluentValidation;

namespace CleanArchitecture.Razor.Application.Projects.Commands.Delete
{
    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidator()
        {
            RuleFor(v => v.Id).NotNull().GreaterThan(0);
        }
    }

    public class DeleteCheckedProjectsCommandValidator : AbstractValidator<DeleteCheckedProjectsCommand>
    {
        public DeleteCheckedProjectsCommandValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty();
        }
    }
}
