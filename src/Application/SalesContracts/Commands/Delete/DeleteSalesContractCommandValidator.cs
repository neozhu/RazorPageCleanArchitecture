using FluentValidation;

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.Delete
{
    public class DeleteSalesContractCommandValidator : AbstractValidator<DeleteSalesContractCommand>
    {
        public DeleteSalesContractCommandValidator()
        {
         
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
           
        }
    }
    public class DeleteCheckedSalesContractsCommandValidator : AbstractValidator<DeleteCheckedSalesContractsCommand>
    {
        public DeleteCheckedSalesContractsCommandValidator()
        {
  
             RuleFor(v => v.Id).NotNull().NotEmpty();
  
        }
    }
}
