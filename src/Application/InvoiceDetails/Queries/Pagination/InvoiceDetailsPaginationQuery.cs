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
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Queries.Pagination
{
    public class InvoiceDetailsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<InvoiceDetailDto>>
    {
       
    }
    
    public class InvoiceDetailsWithPaginationQueryHandler :
         IRequestHandler<InvoiceDetailsWithPaginationQuery, PaginatedData<InvoiceDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<InvoiceDetailsWithPaginationQueryHandler> _localizer;

        public InvoiceDetailsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<InvoiceDetailsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<InvoiceDetailDto>> Handle(InvoiceDetailsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var filters = PredicateBuilder.FromFilter<InvoiceDetail>(request.FilterRules);
           var data = await _context.InvoiceDetails.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<InvoiceDetailDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

