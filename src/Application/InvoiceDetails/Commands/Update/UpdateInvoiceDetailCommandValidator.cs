using FluentValidation;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Update
{
    public class UpdateInvoiceDetailCommandValidator : AbstractValidator<UpdateInvoiceDetailCommand>
    {
        public UpdateInvoiceDetailCommandValidator()
        {
            RuleFor(x => x.SalesContractId)
                .GreaterThan(0);
            RuleFor(v => v.InvoiceNo)
                  .MaximumLength(256)
                  .NotEmpty();

        }
    }
}
