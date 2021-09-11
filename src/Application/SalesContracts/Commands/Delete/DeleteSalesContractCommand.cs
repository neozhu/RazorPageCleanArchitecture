using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.SalesContracts.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.Delete
{
    public class DeleteSalesContractCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedSalesContractsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteSalesContractCommandHandler : 
                 IRequestHandler<DeleteSalesContractCommand, Result>,
                 IRequestHandler<DeleteCheckedSalesContractsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteSalesContractCommandHandler> _localizer;
        public DeleteSalesContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteSalesContractCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteSalesContractCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing DeleteSalesContractCommandHandler method 
           var item = await _context.SalesContracts.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.SalesContracts.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedSalesContractsCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing DeleteCheckedSalesContractsCommandHandler method 
           var items = await _context.SalesContracts.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.SalesContracts.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
