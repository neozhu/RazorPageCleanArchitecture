using FluentValidation;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Update
{
    public class UpdateSalesContractDetailCommandValidator : AbstractValidator<UpdateSalesContractDetailCommand>
    {
        public UpdateSalesContractDetailCommandValidator()
        {

            RuleFor(v => v.SalesContractId)
                   .GreaterThan(0);
            RuleFor(v => v.Terms)
                   .MaximumLength(256)
                   .NotEmpty();

        }
    }
}
