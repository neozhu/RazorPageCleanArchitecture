using FluentValidation;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Import
{
    public class ImportSalesContractDetailsCommandValidator : AbstractValidator<ImportSalesContractDetailsCommand>
    {
        public ImportSalesContractDetailsCommandValidator()
        {
          
            RuleFor(v => v.Data)
                  .NotNull()
                  .NotEmpty();

        }
    }
}
