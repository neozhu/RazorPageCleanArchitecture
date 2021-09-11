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
using CleanArchitecture.Razor.Application.InvoiceDetails.DTOs;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.Queries.Export
{
    public class ExportInvoiceDetailsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportInvoiceDetailsQueryHandler :
         IRequestHandler<ExportInvoiceDetailsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportInvoiceDetailsQueryHandler> _localizer;

        public ExportInvoiceDetailsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportInvoiceDetailsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportInvoiceDetailsQuery request, CancellationToken cancellationToken)
        {
           
            var filters = PredicateBuilder.FromFilter<InvoiceDetail>(request.FilterRules);
            var data = await _context.InvoiceDetails.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<InvoiceDetailDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<InvoiceDetailDto, object>>()
                {
                    { _localizer["Contract No"], item => item.ContractNo },
                    { _localizer["Project Name"], item => item.ProjectName },
                    { _localizer["Customer Name"], item => item.CustomerName },
                    { _localizer["Contract Amount"], item => item.ContractAmount },
                    { _localizer["Invoice Amount"], item => item.InvoiceAmount },
                    { _localizer["Tax Rate"], item => item.TaxRate },
                    { _localizer["Invoice No"], item => item.InvoiceNo },
                    { _localizer["Invoice Date"], item => item.InvoiceDate },
                    { _localizer["Due Time"], item => item.DueTime },
                    { _localizer["Has Paid"], item => item.HasPaid },
                    { _localizer["Balance"], item => item.Balance },
                    { _localizer["Comments"], item => item.Comments },
                }
                , _localizer["InvoiceDetails"]);
            return result;
        }
    }
}

