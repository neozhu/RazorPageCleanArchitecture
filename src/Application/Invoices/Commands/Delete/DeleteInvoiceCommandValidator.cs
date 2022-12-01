namespace CleanArchitecture.Razor.Application.Invoices.Commands.Delete
{
    public class DeleteInvoiceCommandValidator : AbstractValidator<DeleteInvoiceCommand>
    {
        public DeleteInvoiceCommandValidator()
        {
      
           RuleFor(v => v.Id).NotNull().GreaterThan(0);

        }
    }
    public class DeleteCheckedInvoicesCommandValidator : AbstractValidator<DeleteCheckedInvoicesCommand>
    {
        public DeleteCheckedInvoicesCommandValidator()
        {
         
             RuleFor(v => v.Id).NotNull().NotEmpty();

        }
    }
}
