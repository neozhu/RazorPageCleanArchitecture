namespace CleanArchitecture.Razor.Application.Invoices.Commands.AddEdit
{
    public class AddEditInvoiceCommandValidator : AbstractValidator<AddEditInvoiceCommand>
    {
        public AddEditInvoiceCommandValidator()
        {

           RuleFor(v => v.Title)
                 .MaximumLength(256)
                 .NotEmpty();
            RuleFor(v => v.InvoiceNo)
                .MaximumLength(256)
                .NotEmpty();
            RuleFor(v => v.Amount)
                 .NotEmpty();
            RuleFor(v => v.Tax)
                 .NotEmpty();
            RuleFor(v => v.TaxRate)
                 .NotEmpty();
            RuleFor(v => v.InvoiceDate)
                .NotEmpty();
        }
    }
}
