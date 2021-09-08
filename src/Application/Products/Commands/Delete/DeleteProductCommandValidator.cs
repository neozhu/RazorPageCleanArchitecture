using FluentValidation;

namespace CleanArchitecture.Razor.Application.Products.Commands.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            //TODO:Implementing DeleteProductCommandValidator method 
            RuleFor(v => v.Id).NotNull().GreaterThan(0);
        }
    }

    public class DeleteCheckedProductsCommandValidator : AbstractValidator<DeleteCheckedProductsCommand>
    {
        public DeleteCheckedProductsCommandValidator()
        {
            //TODO:Implementing DeleteProductCommandValidator method 
            RuleFor(v => v.Id).NotNull().NotEmpty();
        }
    }
}
