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
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.AddEdit
{
    public class AddEditPurchaseOrderCommand: PurchaseOrderDto,IRequest<Result>, IMapFrom<PurchaseOrder>
    {
        
    }

    public class AddEditPurchaseOrderCommandHandler : IRequestHandler<AddEditPurchaseOrderCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditPurchaseOrderCommandHandler> _localizer;
        public AddEditPurchaseOrderCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditPurchaseOrderCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
  
            if (request.Id > 0)
            {
                var item = await _context.PurchaseOrders.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
            }
            else
            {
                var item = _mapper.Map<PurchaseOrder>(request);
                _context.PurchaseOrders.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
