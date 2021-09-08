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

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Commands.Create
{
    public class CreatePurchaseOrderCommand: PurchaseOrderDto,IRequest<Result>, IMapFrom<PurchaseOrder>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PurchaseOrder, CreatePurchaseOrderCommand>().ReverseMap();
        }
    }
    

    public class CreatePurchaseOrderCommandHandler : IRequestHandler<CreatePurchaseOrderCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreatePurchaseOrderCommand> _localizer;
        public CreatePurchaseOrderCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreatePurchaseOrderCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
  
           var item = _mapper.Map<PurchaseOrder>(request);
            _context.PurchaseOrders.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
