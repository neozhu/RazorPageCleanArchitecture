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
using CleanArchitecture.Razor.Application.Products.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.Products.Queries.Pagination
{
    public class ProductsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<ProductDto>>
    {
       
    }
    
    public class ProductsWithPaginationQueryHandler :
         IRequestHandler<ProductsWithPaginationQuery, PaginatedData<ProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ProductsWithPaginationQueryHandler> _localizer;

        public ProductsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<ProductsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<ProductDto>> Handle(ProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ProductsWithPaginationQueryHandler method 
            var filters = PredicateBuilder.FromFilter<Product>(request.FilterRules);
            var data = await _context.Products.Where(filters)
                         .OrderBy($"{request.Sort} {request.Order}")
                         .ProjectTo<ProductDto> (_mapper.ConfigurationProvider)
                         .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

