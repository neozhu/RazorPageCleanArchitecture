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
using CleanArchitecture.Razor.Application.PurchaseContracts.DTOs;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Queries.Export
{
    public class ExportPurchaseContractsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportPurchaseContractsQueryHandler :
         IRequestHandler<ExportPurchaseContractsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportPurchaseContractsQueryHandler> _localizer;

        public ExportPurchaseContractsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportPurchaseContractsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportPurchaseContractsQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<PurchaseContract>(request.FilterRules);
            var data = await _context.PurchaseContracts.Where(filters)
                       .Include(x => x.Project).Include(x => x.Customer)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<PurchaseContractDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<PurchaseContractDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                    { _localizer["Contract No"], item => item.ContractNo },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Project Name"], item => item.ProjectName },
                    { _localizer["Customer Name"], item => item.CustomerName },
                    { _localizer["Contract Date"], item => item.ContractDate },
                    { _localizer["Closed Date"], item => item.ClosedDate },
                    { _localizer["Order No"], item => item.OrderNo },
                    { _localizer["Contract Amount"], item => item.ContractAmount },
                    { _localizer["Paid Amount"], item => item.PaidAmount },
                    { _localizer["Invoice Amount"], item => item.InvoiceAmount },
                    { _localizer["Balance"], item => item.Balance },
                    { _localizer["Comments"], item => item.Comments }
                }
                , _localizer["PurchaseContracts"]);
            return result;
        }
    }
}

