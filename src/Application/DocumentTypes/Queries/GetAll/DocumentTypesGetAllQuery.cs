// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Razor.Application.DocumentTypes.DTOs;
using CleanArchitecture.Razor.Application.Common.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;
using CleanArchitecture.Razor.Application.DocumentTypes.Caching;

namespace CleanArchitecture.Razor.Application.DocumentTypes.Queries.PaginationQuery
{
    public class DocumentTypesGetAllQuery : IRequest<IEnumerable<DocumentTypeDto>>,ICacheable
    {

        public string CacheKey => DocumentTypeCacheKey.GetAllCacheKey;

        public MemoryCacheEntryOptions Options => null;
    }
    public class DocumentTypesGetAllQueryHandler : IRequestHandler<DocumentTypesGetAllQuery, IEnumerable<DocumentTypeDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DocumentTypesGetAllQueryHandler(
        
            IApplicationDbContext context,
            IMapper mapper
            )
        {
    
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DocumentTypeDto>> Handle(DocumentTypesGetAllQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.DocumentTypes
                .OrderBy(x=>x.Name)
                .ProjectTo<DocumentTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return data;
        }
    }
}
