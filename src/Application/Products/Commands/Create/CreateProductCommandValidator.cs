using FluentValidation;

namespace CleanArchitecture.Razor.Application.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            //TODO:Implementing CreateProductCommandValidator method 
            RuleFor(v => v.Name).NotEmpty();
        }
    }
}
