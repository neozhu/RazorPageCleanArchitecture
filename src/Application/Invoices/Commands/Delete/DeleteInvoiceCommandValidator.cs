using FluentValidation;

namespace CleanArchitecture.Razor.Application.Invoices.Commands.Delete
{
    public class DeleteInvoiceCommandValidator : AbstractValidator<DeleteInvoiceCommand>
    {
        public DeleteInvoiceCommandValidator()
        {
           //TODO:Implementing DeleteInvoiceCommandValidator method 
           //ex. RuleFor(v => v.Id).NotNull().GreaterThan(0);
           throw new System.NotImplementedException();
        }
    }
    public class DeleteCheckedInvoicesCommandValidator : AbstractValidator<DeleteCheckedInvoicesCommand>
    {
        public DeleteCheckedInvoicesCommandValidator()
        {
            //TODO:Implementing DeleteProductCommandValidator method 
            //ex. RuleFor(v => v.Id).NotNull().NotEmpty();
            throw new System.NotImplementedException();
        }
    }
}
