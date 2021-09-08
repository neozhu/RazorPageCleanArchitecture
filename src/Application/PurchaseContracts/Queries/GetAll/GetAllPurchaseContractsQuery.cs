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

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Queries.GetAll
{
    public class GetAllPurchaseContractsQuery : IRequest<IEnumerable<PurchaseContractDto>>
    {
       
    }
    
    public class GetAllPurchaseContractsQueryHandler :
         IRequestHandler<GetAllPurchaseContractsQuery, IEnumerable<PurchaseContractDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllPurchaseContractsQueryHandler> _localizer;

        public GetAllPurchaseContractsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllPurchaseContractsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<PurchaseContractDto>> Handle(GetAllPurchaseContractsQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.PurchaseContracts
                         .ProjectTo<PurchaseContractDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

