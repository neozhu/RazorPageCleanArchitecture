using FluentValidation;

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.Import
{
    public class ImportSalesContractsCommandValidator : AbstractValidator<ImportSalesContractsCommand>
    {
        public ImportSalesContractsCommandValidator()
        {

           RuleFor(v => v.Data)
                  .NotNull()
                  .NotEmpty();

        }
    }
}
