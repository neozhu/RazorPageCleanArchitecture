namespace CleanArchitecture.Razor.Application.Invoices.Commands.Upload
{
    public class UploadInvoicesCommandValidator : AbstractValidator<UploadInvoicesCommand>
    {
        public UploadInvoicesCommandValidator()
        {

            RuleFor(v => v.Data)
                  .NotNull()
                  .NotEmpty();
             

        }
    }
}
