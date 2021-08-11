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
using CleanArchitecture.Razor.Application.KeyValues.DTOs;

namespace CleanArchitecture.Razor.Application.KeyValues.Queries.PaginationQuery
{
    public class KeyValuesWithPaginationQuery : IRequest<PaginatedData<KeyValueDto>>
    {
        public string filterRules { get; set; }
        public int page { get; set; } = 1;
        public int rows { get; set; } = 15;
        public string sort { get; set; } = "Id";
        public string order { get; set; } = "desc";
        
    }
    public class KeyValuesQueryHandler : IRequestHandler<KeyValuesWithPaginationQuery, PaginatedData<KeyValueDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public KeyValuesQueryHandler(
        
            IApplicationDbContext context,
            IMapper mapper
            )
        {
    
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedData<KeyValueDto>> Handle(KeyValuesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<KeyValue>(request.filterRules);
            var data = await _context.KeyValues.Where(filters)
                .OrderBy($"{request.sort} {request.order}")
                .ProjectTo<KeyValueDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.page, request.rows);

            return data;
        }
    }
}
