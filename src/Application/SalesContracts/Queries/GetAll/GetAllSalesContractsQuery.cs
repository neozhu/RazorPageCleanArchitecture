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

namespace CleanArchitecture.Razor.Application.SalesContracts.Queries.GetAll
{
    public class GetAllSalesContractsQuery : IRequest<IEnumerable<SalesContractDto>>
    {
       
    }
    
    public class GetAllSalesContractsQueryHandler :
         IRequestHandler<GetAllSalesContractsQuery, IEnumerable<SalesContractDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllSalesContractsQueryHandler> _localizer;

        public GetAllSalesContractsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllSalesContractsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<SalesContractDto>> Handle(GetAllSalesContractsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.SalesContracts
                         .ProjectTo<SalesContractDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

