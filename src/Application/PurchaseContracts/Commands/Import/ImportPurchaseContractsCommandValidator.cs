using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Import
{
    public class ImportPurchaseContractsCommandValidator : AbstractValidator<ImportPurchaseContractsCommand>
    {
        public ImportPurchaseContractsCommandValidator()
        {
          
           RuleFor(v => v.Data)
                 .NotNull()
                 .NotEmpty();
      
        }
    }
}
