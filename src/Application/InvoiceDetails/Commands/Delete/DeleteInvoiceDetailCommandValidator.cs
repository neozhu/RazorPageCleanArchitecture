using FluentValidation;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Delete
{
    public class DeleteInvoiceDetailCommandValidator : AbstractValidator<DeleteInvoiceDetailCommand>
    {
        public DeleteInvoiceDetailCommandValidator()
        {
        
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
       
        }
    }
    public class DeleteCheckedInvoiceDetailsCommandValidator : AbstractValidator<DeleteCheckedInvoiceDetailsCommand>
    {
        public DeleteCheckedInvoiceDetailsCommandValidator()
        {
            
            RuleFor(v => v.Id).NotNull().NotEmpty();
          
        }
    }
}
