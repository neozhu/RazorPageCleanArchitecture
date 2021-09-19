// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.Caching;
using CleanArchitecture.Razor.Application.Constants;
using CleanArchitecture.Razor.Application.KeyValues.Caching;
using CleanArchitecture.Razor.Application.KeyValues.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace CleanArchitecture.Razor.Application.KeyValues.Queries.ByName
{
    public class KeyValuesQueryByName:IRequest<IEnumerable<KeyValueDto>>, ICacheable
    {
        public string Name { get; set; }

        public string CacheKey =>Cache.GetKeyValuesCacheKey(Name);

        public MemoryCacheEntryOptions Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(ExpirationTokenSource.ResetCacheToken.Token));
    }
    public class KeyValuesQueryByNameHandler : IRequestHandler<KeyValuesQueryByName, IEnumerable<KeyValueDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public KeyValuesQueryByNameHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<KeyValueDto>> Handle(KeyValuesQueryByName request, CancellationToken cancellationToken)
        {
            var data = await _context.KeyValues.Where(x => x.Name == request.Name)
               .ProjectTo<KeyValueDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
            return data;
        }
    }
}
