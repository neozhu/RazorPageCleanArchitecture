using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.InvoiceDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.AddEdit
{
    public class AddEditInvoiceDetailCommand: InvoiceDetailDto,IRequest<Result>, IMapFrom<InvoiceDetail>
    {
      
    }

    public class AddEditInvoiceDetailCommandHandler : IRequestHandler<AddEditInvoiceDetailCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditInvoiceDetailCommandHandler> _localizer;
        public AddEditInvoiceDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditInvoiceDetailCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditInvoiceDetailCommand request, CancellationToken cancellationToken)
        {
         
            if (request.Id > 0)
            {
                var item = await _context.InvoiceDetails.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                var updateEvent = new InvoiceDetailUpdatedEvent(item);
                item.DomainEvents.Add(updateEvent);
            }
            else
            {
                var item = _mapper.Map<InvoiceDetail>(request);
                _context.InvoiceDetails.Add(item);
                var createdEvent = new InvoiceDetailCreatedEvent(item);
                item.DomainEvents.Add(createdEvent);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
