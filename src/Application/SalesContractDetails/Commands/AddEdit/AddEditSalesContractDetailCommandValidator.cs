using FluentValidation;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.AddEdit
{
    public class AddEditSalesContractDetailCommandValidator : AbstractValidator<AddEditSalesContractDetailCommand>
    {
        public AddEditSalesContractDetailCommandValidator()
        {
            RuleFor(v => v.SalesContractId)
                     .GreaterThan(0);
            RuleFor(v => v.Terms)
                   .MaximumLength(256)
                   .NotEmpty();
        }
    }
}
