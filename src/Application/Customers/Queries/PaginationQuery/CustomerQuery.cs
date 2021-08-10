// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Domain.Entities;
using System.Linq.Dynamic.Core;
using MediatR;
using CleanArchitecture.Razor.Application.Common.Mappings;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Razor.Application.Customers.Queries.PaginationQuery
{
    public class CustomersWithPaginationQuery : IRequest<PaginatedData<CustomerDto>>
    {
        public string filterRules { get; set; }
        public int page { get; set; } = 1;
        public int rows { get; set; } = 15;
        public string sort { get; set; } = "Id";
        public string order { get; set; } = "desc";
        
    }
    public class CustomersQueryHandler : IRequestHandler<CustomersWithPaginationQuery, PaginatedData<CustomerDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersQueryHandler(
        
            IApplicationDbContext context,
            IMapper mapper
            )
        {
    
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedData<CustomerDto>> Handle(CustomersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<Customer>(request.filterRules);
            var data = await _context.Customers.Where(filters)
                .OrderBy($"{request.sort} {request.order}")
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.page, request.rows);

            return data;
        }
    }
}
