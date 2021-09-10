using FluentValidation;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.AddEdit
{
    public class AddEditInvoiceDetailCommandValidator : AbstractValidator<AddEditInvoiceDetailCommand>
    {
        public AddEditInvoiceDetailCommandValidator()
        {
            RuleFor(x => x.SalesContractId)
                 .GreaterThan(0);
            RuleFor(v => v.InvoiceNo)
                  .MaximumLength(256)
                  .NotEmpty();
        }
    }
}
