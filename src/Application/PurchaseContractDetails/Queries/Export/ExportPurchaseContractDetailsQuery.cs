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
using CleanArchitecture.Razor.Application.PurchaseContractDetails.DTOs;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Queries.Export
{
    public class ExportPurchaseContractDetailsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportPurchaseContractDetailsQueryHandler :
         IRequestHandler<ExportPurchaseContractDetailsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportPurchaseContractDetailsQueryHandler> _localizer;

        public ExportPurchaseContractDetailsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportPurchaseContractDetailsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportPurchaseContractDetailsQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ExportPurchaseContractDetailsQueryHandler method 
            var filters = PredicateBuilder.FromFilter<PurchaseContractDetail>(request.FilterRules);
            var data = await _context.PurchaseContractDetails.Where(filters)
                       .OrderBy("{request.Sort} {request.Order}")
                       .ProjectTo<PurchaseContractDetailDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<PurchaseContractDetailDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                }
                , _localizer["PurchaseContractDetails"]);
            return result;
        }
    }
}

