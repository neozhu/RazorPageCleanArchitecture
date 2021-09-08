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
using CleanArchitecture.Razor.Application.PurchaseContracts.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Queries.Pagination
{
    public class PurchaseContractsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<PurchaseContractDto>>
    {
       
    }
    
    public class PurchaseContractsWithPaginationQueryHandler :
         IRequestHandler<PurchaseContractsWithPaginationQuery, PaginatedData<PurchaseContractDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PurchaseContractsWithPaginationQueryHandler> _localizer;

        public PurchaseContractsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<PurchaseContractsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<PurchaseContractDto>> Handle(PurchaseContractsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var filters = PredicateBuilder.FromFilter<PurchaseContract>(request.FilterRules);
           var data = await _context.PurchaseContracts.Where(filters)
                .Include(x=>x.Project).Include(x=>x.Customer)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<PurchaseContractDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

