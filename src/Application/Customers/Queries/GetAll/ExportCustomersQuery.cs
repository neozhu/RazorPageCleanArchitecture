// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
using CleanArchitecture.Razor.Application.Customers.DTOs;
using CleanArchitecture.Razor.Application.Common.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;
using CleanArchitecture.Razor.Application.Constants;
using Microsoft.Extensions.Primitives;
using CleanArchitecture.Razor.Application.Customers.Caching;

namespace CleanArchitecture.Razor.Application.Customers.Queries.GetAll
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>, ICacheable
    {
        public string CacheKey => Cache.GetAllCustomersCacheKey;

        public MemoryCacheEntryOptions Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(CustomerCacheTokenSource.ResetCacheToken.Token));
    }

    public class GetAllCustomersQueryQueryHandler :
         IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllCustomersQueryQueryHandler> _localizer;

        public GetAllCustomersQueryQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllCustomersQueryQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
