// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
using CleanArchitecture.Razor.Application.DocumentTypes.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using CleanArchitecture.Razor.Application.DocumentTypes.Caching;
using CleanArchitecture.Razor.Application.Common.Interfaces.Caching;

namespace CleanArchitecture.Razor.Application.DocumentTypes.Queries.PaginationQuery
{
    public class DocumentTypesWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<DocumentTypeDto>>, ICacheable
    {
        public string CacheKey => $"DocumentTypesWithPaginationQuery,{this.ToString()}";

        public MemoryCacheEntryOptions Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(DocumentTypeCacheTokenSource.ResetCacheToken.Token));
    }
    public class DocumentTypesQueryHandler : IRequestHandler<DocumentTypesWithPaginationQuery, PaginatedData<DocumentTypeDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DocumentTypesQueryHandler(
        
            IApplicationDbContext context,
            IMapper mapper
            )
        {
    
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedData<DocumentTypeDto>> Handle(DocumentTypesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<DocumentType>(request.FilterRules);
            var data = await _context.DocumentTypes.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<DocumentTypeDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);

            return data;
        }
    }
}
