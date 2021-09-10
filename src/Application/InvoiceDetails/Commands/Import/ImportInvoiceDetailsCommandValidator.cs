using FluentValidation;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Import
{
    public class ImportInvoiceDetailsCommandValidator : AbstractValidator<ImportInvoiceDetailsCommand>
    {
        public ImportInvoiceDetailsCommandValidator()
        {

           RuleFor(v => v.Data)
                  .NotNull()
                  .NotEmpty();

        }
    }
}
