using FluentValidation;

namespace CleanArchitecture.Razor.Application.Products.Commands.Import
{
    public class ImportProductsCommandValidator : AbstractValidator<ImportProductsCommand>
    {
        public ImportProductsCommandValidator()
        {
            
            RuleFor(v => v.Data).NotNull();
        }
    }
}
