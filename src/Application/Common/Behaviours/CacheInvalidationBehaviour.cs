// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces.Caching;
using LazyCache;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.Common.Behaviours
{
    public class CacheInvalidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : ICacheInvalidator
    {
        private readonly IAppCache _cache;
        private readonly ILogger<CacheInvalidationBehaviour<TRequest, TResponse>> _logger;

        public CacheInvalidationBehaviour(
            IAppCache cache,
            ILogger<CacheInvalidationBehaviour<TRequest, TResponse>> logger
            )
        {
            _cache = cache;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            _logger.LogInformation("{Request} is configured for cache expire.", requestName);

            var response = await next();
            if (!string.IsNullOrEmpty(request.CacheKey))
            {
                _cache.Remove(request.CacheKey);
            }
            request.CancellationTokenSource?.Cancel();
        
            return response;
        }
    }
}
