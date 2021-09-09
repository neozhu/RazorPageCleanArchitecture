using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.PurchaseContractDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.AddEdit
{
    public class AddEditPurchaseContractDetailCommand: PurchaseContractDetailDto,IRequest<Result<int>>, IMapFrom<PurchaseContractDetail>
    {
      
    }

    public class AddEditPurchaseContractDetailCommandHandler : IRequestHandler<AddEditPurchaseContractDetailCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditPurchaseContractDetailCommandHandler> _localizer;
        public AddEditPurchaseContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditPurchaseContractDetailCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditPurchaseContractDetailCommand request, CancellationToken cancellationToken)
        {
           var  Id = 0;
            if (request.Id > 0)
            {
                var item = await _context.PurchaseContractDetails.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                var updateEvent = new PurchaseContractDetailUpdatedEvent(item);
                item.DomainEvents.Add(updateEvent);
                Id = item.Id;
            }
            else
            {
                var item = _mapper.Map<PurchaseContractDetail>(request);
                _context.PurchaseContractDetails.Add(item);
                Id = item.Id;

                var createdEvent = new PurchaseContractDetailCreatedEvent(item);
                item.DomainEvents.Add(createdEvent);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(Id);
        }
    }
}
