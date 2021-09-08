using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.AddEdit
{
    public class AddEditPurchaseContractDetailCommandValidator : AbstractValidator<AddEditPurchaseContractDetailCommand>
    {
        public AddEditPurchaseContractDetailCommandValidator()
        {
            
            RuleFor(v => v.Terms)
                  .MaximumLength(256)
                  .NotEmpty();
            RuleFor(v => v.PurchaseContractId)
                  .GreaterThan(0);

        }
    }
}
