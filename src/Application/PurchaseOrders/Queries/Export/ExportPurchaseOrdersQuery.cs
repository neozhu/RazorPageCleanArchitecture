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
using CleanArchitecture.Razor.Application.PurchaseOrders.DTOs;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Queries.Export
{
    public class ExportPurchaseOrdersQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportPurchaseOrdersQueryHandler :
         IRequestHandler<ExportPurchaseOrdersQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportPurchaseOrdersQueryHandler> _localizer;

        public ExportPurchaseOrdersQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportPurchaseOrdersQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportPurchaseOrdersQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ExportPurchaseOrdersQueryHandler method 
            var filters = PredicateBuilder.FromFilter<PurchaseOrder>(request.FilterRules);
            var data = await _context.PurchaseOrders.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<PurchaseOrderDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<PurchaseOrderDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                }
                , _localizer["PurchaseOrders"]);
            return result;
        }
    }
}

