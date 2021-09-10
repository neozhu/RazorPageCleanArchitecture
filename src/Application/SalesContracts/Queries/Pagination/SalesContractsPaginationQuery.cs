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
using CleanArchitecture.Razor.Application.SalesContracts.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.SalesContracts.Queries.Pagination
{
    public class SalesContractsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<SalesContractDto>>
    {
       
    }
    
    public class SalesContractsWithPaginationQueryHandler :
         IRequestHandler<SalesContractsWithPaginationQuery, PaginatedData<SalesContractDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesContractsWithPaginationQueryHandler> _localizer;

        public SalesContractsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesContractsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<SalesContractDto>> Handle(SalesContractsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var filters = PredicateBuilder.FromFilter<SalesContract>(request.FilterRules);
           var data = await _context.SalesContracts.Where(filters)
                .OrderBy("{request.Sort} {request.Order}")
                .ProjectTo<SalesContractDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

