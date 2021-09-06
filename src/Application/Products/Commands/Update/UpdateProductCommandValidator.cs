using FluentValidation;

namespace CleanArchitecture.Razor.Application.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
           //TODO:Implementing UpdateProductCommandValidator method 
           RuleFor(v=>v.Id).GreaterThan(0);
        }
    }
}
