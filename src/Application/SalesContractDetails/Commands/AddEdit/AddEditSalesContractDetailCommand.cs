using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.SalesContractDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.AddEdit
{
    public class AddEditSalesContractDetailCommand: SalesContractDetailDto,IRequest<Result<int>>, IMapFrom<SalesContractDetail>
    {
      
    }

    public class AddEditSalesContractDetailCommandHandler : IRequestHandler<AddEditSalesContractDetailCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditSalesContractDetailCommandHandler> _localizer;
        public AddEditSalesContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditSalesContractDetailCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditSalesContractDetailCommand request, CancellationToken cancellationToken)
        {
            var Id = 0;
            if (request.Id > 0)
            {
                var item = await _context.SalesContractDetails.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                var updateEvent = new SalesContractDetailUpdatedEvent(item);
                item.DomainEvents.Add(updateEvent);
                Id = item.Id;
            }
            else
            {
                var item = _mapper.Map<SalesContractDetail>(request);
                _context.SalesContractDetails.Add(item);
                Id = item.Id;
                var createdEvent = new SalesContractDetailCreatedEvent(item);
                item.DomainEvents.Add(createdEvent);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(Id);
        }
    }
}
