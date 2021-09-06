using FluentValidation;

namespace CleanArchitecture.Razor.Application.Products.Commands.AddEdit
{
    public class AddEditProductCommandValidator : AbstractValidator<AddEditProductCommand>
    {
        public AddEditProductCommandValidator()
        {
            //TODO:Implementing AddEditProductCommandValidator method 
            RuleFor(v => v.Name).NotEmpty();
        }
    }
}
