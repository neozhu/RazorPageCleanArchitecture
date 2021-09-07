using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.Import
{
    public class ImportPurchaseOrdersCommandValidator : AbstractValidator<ImportPurchaseOrdersCommand>
    {
        public ImportPurchaseOrdersCommandValidator()
        {
     
            RuleFor(v => v.Data)
                 .NotNull()
                 .NotEmpty();
      
        }
    }
}
