using FluentValidation;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Delete
{
    public class DeleteSalesContractDetailCommandValidator : AbstractValidator<DeleteSalesContractDetailCommand>
    {
        public DeleteSalesContractDetailCommandValidator()
        {
         
           RuleFor(v => v.Id).NotNull().GreaterThan(0);

        }
    }
    public class DeleteCheckedSalesContractDetailsCommandValidator : AbstractValidator<DeleteCheckedSalesContractDetailsCommand>
    {
        public DeleteCheckedSalesContractDetailsCommandValidator()
        {
        
           RuleFor(v => v.Id).NotNull().NotEmpty();
     
        }
    }
}
