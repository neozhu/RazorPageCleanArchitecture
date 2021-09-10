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

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Commands.Update
{
    public class UpdateInvoiceDetailCommand: InvoiceDetailDto,IRequest<Result>, IMapFrom<InvoiceDetail>
    {
        
    }

    public class UpdateInvoiceDetailCommandHandler : IRequestHandler<UpdateInvoiceDetailCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateInvoiceDetailCommandHandler> _localizer;
        public UpdateInvoiceDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateInvoiceDetailCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateInvoiceDetailCommand request, CancellationToken cancellationToken)
        {
           var item =await _context.InvoiceDetails.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }
}
