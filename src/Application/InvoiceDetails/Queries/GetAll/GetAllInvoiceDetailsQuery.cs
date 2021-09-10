using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Domain.Entities;
using System.Linq.Dynamic.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.InvoiceDetails.DTOs;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Queries.GetAll
{
    public class GetAllInvoiceDetailsQuery : IRequest<IEnumerable<InvoiceDetailDto>>
    {
       
    }
    
    public class GetAllInvoiceDetailsQueryHandler :
         IRequestHandler<GetAllInvoiceDetailsQuery, IEnumerable<InvoiceDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllInvoiceDetailsQueryHandler> _localizer;

        public GetAllInvoiceDetailsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllInvoiceDetailsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<InvoiceDetailDto>> Handle(GetAllInvoiceDetailsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.InvoiceDetails
                         .ProjectTo<InvoiceDetailDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

