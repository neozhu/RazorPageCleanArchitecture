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
using CleanArchitecture.Razor.Application.SalesContracts.DTOs;

namespace CleanArchitecture.Razor.Application.SalesContracts.Queries.Export
{
    public class ExportSalesContractsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportSalesContractsQueryHandler :
         IRequestHandler<ExportSalesContractsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportSalesContractsQueryHandler> _localizer;

        public ExportSalesContractsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportSalesContractsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportSalesContractsQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<SalesContract>(request.FilterRules);
            var data = await _context.SalesContracts.Where(filters)
                       .OrderBy("{request.Sort} {request.Order}")
                       .ProjectTo<SalesContractDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<SalesContractDto, object>>()
                {
                    { _localizer["Contract No"], item => item.ContractNo },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Project Name"], item => item.ProjectName },
                    { _localizer["Customer Name"], item => item.CustomerName },
                    { _localizer["Contract Date"], item => item.ContractDate },
                    { _localizer["Closed Date"], item => item.ClosedDate },
                    { _localizer["Order No"], item => item.OrderNo },
                    { _localizer["Contract Amount"], item => item.ContractAmount },
                    { _localizer["Tax Rate"], item => item.TaxRate },
                    { _localizer["Receipt Amount"], item => item.ReceiptAmount },
                    { _localizer["Invoice Amount"], item => item.InvoiceAmount },
                    { _localizer["Balance"], item => item.Balance },
                    { _localizer["Comments"], item => item.Comments }
      

                }
                , _localizer["SalesContracts"]);
            return result;
        }
    }
}

