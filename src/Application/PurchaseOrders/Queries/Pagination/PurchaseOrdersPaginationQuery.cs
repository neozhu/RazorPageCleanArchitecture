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
using CleanArchitecture.Razor.Application.PurchaseOrders.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Queries.Pagination
{
    public class PurchaseOrdersWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<PurchaseOrderDto>>
    {
       
    }
    
    public class PurchaseOrdersWithPaginationQueryHandler :
         IRequestHandler<PurchaseOrdersWithPaginationQuery, PaginatedData<PurchaseOrderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PurchaseOrdersWithPaginationQueryHandler> _localizer;

        public PurchaseOrdersWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<PurchaseOrdersWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<PurchaseOrderDto>> Handle(PurchaseOrdersWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var filters = PredicateBuilder.FromFilter<PurchaseOrder>(request.FilterRules);
           var data = await _context.PurchaseOrders.Where(filters)
                .Include(x=>x.Customer).Include(x=>x.Product)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<PurchaseOrderDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

