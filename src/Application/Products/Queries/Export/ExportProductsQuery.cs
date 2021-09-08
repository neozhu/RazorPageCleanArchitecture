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

namespace CleanArchitecture.Razor.Application.Products.Queries.Export
{
    public class ExportProductsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportProductsQueryHandler :
         IRequestHandler<ExportProductsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExportProductsQueryHandler> _localizer;

        public ExportProductsQueryHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IMapper mapper,
            IStringLocalizer<ExportProductsQueryHandler> localizer
            )
        {
            _context = context;
            _excelService = excelService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportProductsQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ExportProductsQueryHandler method 
            var filters = PredicateBuilder.FromFilter<Product>(request.FilterRules);
            var data = await _context.Products.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<ProductDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                    { _localizer["Name"], item => item.Name },
                    { _localizer["Description"], item => item.Description }
                }
                , _localizer["Products"]);
            return result;
        }
    }
}

