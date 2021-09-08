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
using CleanArchitecture.Razor.Application.PurchaseContractDetails.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Queries.Pagination
{
    public class PurchaseContractDetailsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<PurchaseContractDetailDto>>
    {
       
    }
    
    public class PurchaseContractDetailsWithPaginationQueryHandler :
         IRequestHandler<PurchaseContractDetailsWithPaginationQuery, PaginatedData<PurchaseContractDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PurchaseContractDetailsWithPaginationQueryHandler> _localizer;

        public PurchaseContractDetailsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<PurchaseContractDetailsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<PurchaseContractDetailDto>> Handle(PurchaseContractDetailsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing PurchaseContractDetailsWithPaginationQueryHandler method 
           var filters = PredicateBuilder.FromFilter<PurchaseContractDetail>(request.FilterRules);
           var data = await _context.PurchaseContractDetails.Where(filters)
                .OrderBy("{request.Sort} {request.Order}")
                .ProjectTo<PurchaseContractDetailDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

