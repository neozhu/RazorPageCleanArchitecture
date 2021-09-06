using FluentValidation;

namespace CleanArchitecture.Razor.Application.Products.Commands.Import
{
    public class ImportProductsCommandValidator : AbstractValidator<ImportProductsCommand>
    {
        public ImportProductsCommandValidator()
        {
            //TODO:Implementing ImportProductCommandValidator method 
            RuleFor(v => v.Data).NotNull();
        }
    }
}
