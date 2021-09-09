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

namespace CleanArchitecture.Razor.Application.Customers.Queries.Export
{
    public class ExportCustomersQuery : IRequest<byte[]>
    {
        public string filterRules { get; set; }
        public string sort { get; set; } = "Id";
        public string order { get; set; } = "desc";
    }
    
    public class ExportCustomersQueryHandler :
         IRequestHandler<ExportCustomersQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportCustomersQueryHandler> _localizer;

        public ExportCustomersQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportCustomersQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        public async Task<byte[]> Handle(ExportCustomersQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<Customer>(request.filterRules);
            var data = await _context.Customers.Where(filters)
                .OrderBy($"{request.sort} {request.order}")
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<CustomerDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                    { _localizer["Name"], item => item.Name },
                    { _localizer["Partner Type"], item => item.PartnerType },
                    { _localizer["Region"], item => item.Region },
                    { _localizer["Sales"], item => item.Sales },
                    { _localizer["Address"], item => item.Address },
                    { _localizer["Contact"], item => item.Contact },
                    { _localizer["Email"], item => item.Email },
                    { _localizer["Phone Number"], item => item.PhoneNumber },
                    { _localizer["Contact2"], item => item.Contact2 },
                    { _localizer["Email2"], item => item.Email2 },
                    { _localizer["Phone Number2"], item => item.PhoneNumber2 },
                    { _localizer["Fax"], item => item.Fax },
                    { _localizer["Tax No"], item => item.TaxNo },
                    { _localizer["Bank"], item => item.Bank },
                    { _localizer["Account No"], item => item.AccountNo },
                    { _localizer["Comments"], item => item.Comments },

                }, _localizer["Customers"]
                );
            return result;
        }

        
    }
}
