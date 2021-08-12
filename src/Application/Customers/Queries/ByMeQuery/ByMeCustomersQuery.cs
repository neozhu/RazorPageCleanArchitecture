// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Domain.Entities;
using System.Linq.Dynamic.Core;
using MediatR;
using CleanArchitecture.Razor.Application.Common.Mappings;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Razor.Application.Customers.DTOs;
using System;
using System.Collections.Generic;
using CleanArchitecture.Razor.Application.Common.Specification;

namespace CleanArchitecture.Razor.Application.Customers.Queries.PaginationQuery
{
    public class ByMeCustomersQuery : IRequest<PaginatedData<CustomerDto>>
    {
        public string UserId { get; set; }
        public string filterRules { get; set; }
        public int page { get; set; } = 1;
        public int rows { get; set; } = 15;
        public string sort { get; set; } = "Id";
        public string order { get; set; } = "desc";
        
    }
    public class ByMeCustomersQueryHandler : IRequestHandler<ByMeCustomersQuery, PaginatedData<CustomerDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ByMeCustomersQueryHandler(
        
            IApplicationDbContext context,
            IMapper mapper
            )
        {
    
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedData<CustomerDto>> Handle(ByMeCustomersQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<Customer>(request.filterRules);
            var data = await _context.Customers.Specify(new CustomerByMeQuerySpecification(request.UserId))
                .Where(filters)
                .OrderBy($"{request.sort} {request.order}")
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.page, request.rows);

            return data;
        }

        public class CustomerByMeQuerySpecification : QuerySpecification<Customer>
        {
            public CustomerByMeQuerySpecification(string userId)
            {
                Criteria = p => p.CreatedBy == userId;
            }

         
        }
    }
}
