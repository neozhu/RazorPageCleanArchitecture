using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.Delete
{
    public class DeletePurchaseOrderCommandValidator : AbstractValidator<DeletePurchaseOrderCommand>
    {
        public DeletePurchaseOrderCommandValidator()
        {
         
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
       
        }
    }
    public class DeleteCheckedPurchaseOrdersCommandValidator : AbstractValidator<DeleteCheckedPurchaseOrdersCommand>
    {
        public DeleteCheckedPurchaseOrdersCommandValidator()
        {
      
            RuleFor(v => v.Id).NotNull().NotEmpty();
  
        }
    }
}
