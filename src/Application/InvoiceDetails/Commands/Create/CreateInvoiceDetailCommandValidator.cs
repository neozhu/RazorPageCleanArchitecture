using FluentValidation;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Create
{
    public class CreateInvoiceDetailCommandValidator : AbstractValidator<CreateInvoiceDetailCommand>
    {
        public CreateInvoiceDetailCommandValidator()
        {
            RuleFor(x => x.SalesContractId)
                 .GreaterThan(0);
            RuleFor(v => v.InvoiceNo)
                  .MaximumLength(256)
                  .NotEmpty();
        }
    }
}
