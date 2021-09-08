using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.PurchaseOrders.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.Delete
{
    public class DeletePurchaseOrderCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedPurchaseOrdersCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeletePurchaseOrderCommandHandler : 
                 IRequestHandler<DeletePurchaseOrderCommand, Result>,
                 IRequestHandler<DeleteCheckedPurchaseOrdersCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeletePurchaseOrderCommandHandler> _localizer;
        public DeletePurchaseOrderCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeletePurchaseOrderCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
      
           var item = await _context.PurchaseOrders.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.PurchaseOrders.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedPurchaseOrdersCommand request, CancellationToken cancellationToken)
        {
      
           var items = await _context.PurchaseOrders.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.PurchaseOrders.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
