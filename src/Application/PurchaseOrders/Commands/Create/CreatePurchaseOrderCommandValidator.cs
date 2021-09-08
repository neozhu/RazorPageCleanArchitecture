using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.Create
{
    public class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
    {
        public CreatePurchaseOrderCommandValidator()
        {
            RuleFor(v => v.PO)
                   .MaximumLength(256)
                   .NotEmpty();
            RuleFor(v => v.ProductId)
                .NotNull();
            RuleFor(v => v.CustomerId)
                .NotNull();
            RuleFor(v => v.OrderDate)
                .NotNull();
            RuleFor(v => v.Amount)
                .NotNull();

        }
    }
}
