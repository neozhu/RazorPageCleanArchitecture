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

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Queries.GetAll
{
    public class GetAllPurchaseContractDetailsQuery : IRequest<IEnumerable<PurchaseContractDetailDto>>
    {
       
    }
    
    public class GetAllPurchaseContractDetailsQueryHandler :
         IRequestHandler<GetAllPurchaseContractDetailsQuery, IEnumerable<PurchaseContractDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllPurchaseContractDetailsQueryHandler> _localizer;

        public GetAllPurchaseContractDetailsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllPurchaseContractDetailsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<PurchaseContractDetailDto>> Handle(GetAllPurchaseContractDetailsQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing GetAllPurchaseContractDetailsQueryHandler method 
            var data = await _context.PurchaseContractDetails
                         .ProjectTo<PurchaseContractDetailDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

