using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Update
{
    public class UpdatePurchaseContractCommandValidator : AbstractValidator<UpdatePurchaseContractCommand>
    {
        public UpdatePurchaseContractCommandValidator()
        {
          
           RuleFor(v => v.ContractNo)
                 .MaximumLength(256)
                 .NotEmpty();
            RuleFor(v => v.ProjectId)
                 .GreaterThan(0);
            RuleFor(v => v.CustomerId)
                .GreaterThan(0);
            RuleFor(v => v.ContractDate)
                 .NotEmpty();
            RuleFor(v => v.ContractAmount)
                 .GreaterThan(0);

        }
    }
}
