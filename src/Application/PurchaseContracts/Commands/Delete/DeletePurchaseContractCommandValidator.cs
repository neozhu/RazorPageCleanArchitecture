using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Delete
{
    public class DeletePurchaseContractCommandValidator : AbstractValidator<DeletePurchaseContractCommand>
    {
        public DeletePurchaseContractCommandValidator()
        {
         
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
         
        }
    }
    public class DeleteCheckedPurchaseContractsCommandValidator : AbstractValidator<DeleteCheckedPurchaseContractsCommand>
    {
        public DeleteCheckedPurchaseContractsCommandValidator()
        {
            
            RuleFor(v => v.Id).NotNull().NotEmpty();

        }
    }
}
