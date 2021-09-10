using FluentValidation;

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.AddEdit
{
    public class AddEditSalesContractCommandValidator : AbstractValidator<AddEditSalesContractCommand>
    {
        public AddEditSalesContractCommandValidator()
        {
            RuleFor(v => v.ContractNo)
                    .MaximumLength(256)
                    .NotEmpty();
            RuleFor(v => v.ProjectId)
                .GreaterThan(0);
            RuleFor(v => v.CustomerId)
                .GreaterThan(0);
            RuleFor(v => v.ContractAmount)
                .GreaterThan(0);
            RuleFor(v => v.ContractDate)
                .NotNull();
        }
    }
}
