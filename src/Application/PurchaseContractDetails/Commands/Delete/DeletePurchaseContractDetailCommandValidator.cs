using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.Delete
{
    public class DeletePurchaseContractDetailCommandValidator : AbstractValidator<DeletePurchaseContractDetailCommand>
    {
        public DeletePurchaseContractDetailCommandValidator()
        {
           //TODO:Implementing DeletePurchaseContractDetailCommandValidator method 
           //ex. RuleFor(v => v.Id).NotNull().GreaterThan(0);
           throw new System.NotImplementedException();
        }
    }
    public class DeleteCheckedPurchaseContractDetailsCommandValidator : AbstractValidator<DeleteCheckedPurchaseContractDetailsCommand>
    {
        public DeleteCheckedPurchaseContractDetailsCommandValidator()
        {
            //TODO:Implementing DeleteProductCommandValidator method 
            //ex. RuleFor(v => v.Id).NotNull().NotEmpty();
            throw new System.NotImplementedException();
        }
    }
}
