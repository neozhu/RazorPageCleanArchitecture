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
using CleanArchitecture.Razor.Application.SalesContractDetails.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Queries.Pagination
{
    public class SalesContractDetailsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<SalesContractDetailDto>>
    {
       
    }
    
    public class SalesContractDetailsWithPaginationQueryHandler :
         IRequestHandler<SalesContractDetailsWithPaginationQuery, PaginatedData<SalesContractDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesContractDetailsWithPaginationQueryHandler> _localizer;

        public SalesContractDetailsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesContractDetailsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<SalesContractDetailDto>> Handle(SalesContractDetailsWithPaginationQuery request, CancellationToken cancellationToken)
        {

           var filters = PredicateBuilder.FromFilter<SalesContractDetail>(request.FilterRules);
           var data = await _context.SalesContractDetails.Where(filters)
                .OrderBy("{request.Sort} {request.Order}")
                .ProjectTo<SalesContractDetailDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

