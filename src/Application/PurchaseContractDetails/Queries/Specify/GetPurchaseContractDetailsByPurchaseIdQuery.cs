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

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Queries.Specify
{
    public class GetPurchaseContractDetailsByPurchaseIdQuery : IRequest<IEnumerable<PurchaseContractDetailDto>>
    {
       public int PurchaseContractId { get; set; }
    }
    
    public class GetPurchaseContractDetailsByPurchaseIdQueryHandler :
         IRequestHandler<GetPurchaseContractDetailsByPurchaseIdQuery, IEnumerable<PurchaseContractDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetPurchaseContractDetailsByPurchaseIdQueryHandler> _localizer;

        public GetPurchaseContractDetailsByPurchaseIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetPurchaseContractDetailsByPurchaseIdQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<PurchaseContractDetailDto>> Handle(GetPurchaseContractDetailsByPurchaseIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.PurchaseContractDetails
                         .Where(x=>x.PurchaseContractId==request.PurchaseContractId)
                         .Include(x=>x.PurchaseContract).ThenInclude(x=>x.Project)
                         .Include(x=>x.PurchaseContract).ThenInclude(x=>x.Customer)
                         .ProjectTo<PurchaseContractDetailDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

