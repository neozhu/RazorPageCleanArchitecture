using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.PurchaseContracts.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Delete
{
    public class DeletePurchaseContractCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedPurchaseContractsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeletePurchaseContractCommandHandler : 
                 IRequestHandler<DeletePurchaseContractCommand, Result>,
                 IRequestHandler<DeleteCheckedPurchaseContractsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeletePurchaseContractCommandHandler> _localizer;
        public DeletePurchaseContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeletePurchaseContractCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeletePurchaseContractCommand request, CancellationToken cancellationToken)
        {
         
           var item = await _context.PurchaseContracts.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.PurchaseContracts.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedPurchaseContractsCommand request, CancellationToken cancellationToken)
        {
       
           var items = await _context.PurchaseContracts.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.PurchaseContracts.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
