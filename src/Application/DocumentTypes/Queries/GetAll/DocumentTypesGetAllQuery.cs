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
using CleanArchitecture.Razor.Application.DocumentTypes.DTOs;
using CleanArchitecture.Razor.Application.Common.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;
using CleanArchitecture.Razor.Application.Constants;

namespace CleanArchitecture.Razor.Application.DocumentTypes.Queries.PaginationQuery
{
    public class DocumentTypesGetAllQuery : IRequest<IEnumerable<DocumentTypeDto>>,ICacheable
    {

        public string CacheKey => Cache.GetAllDocumentTypesCacheKey;

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
