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
using CleanArchitecture.Razor.Application.SalesContractDetails.DTOs;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Queries.Export
{
    public class ExportSalesContractDetailsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportSalesContractDetailsQueryHandler :
         IRequestHandler<ExportSalesContractDetailsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportSalesContractDetailsQueryHandler> _localizer;

        public ExportSalesContractDetailsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportSalesContractDetailsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportSalesContractDetailsQuery request, CancellationToken cancellationToken)
        {
      
            var filters = PredicateBuilder.FromFilter<SalesContractDetail>(request.FilterRules);
            var data = await _context.SalesContractDetails.Where(filters)
                       .OrderBy("{request.Sort} {request.Order}")
                       .ProjectTo<SalesContractDetailDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<SalesContractDetailDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                }
                , _localizer["SalesContractDetails"]);
            return result;
        }
    }
}

