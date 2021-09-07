using FluentValidation;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.AddEdit
{
    public class AddEditPurchaseOrderCommandValidator : AbstractValidator<AddEditPurchaseOrderCommand>
    {
        public AddEditPurchaseOrderCommandValidator()
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
