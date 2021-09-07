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
using CleanArchitecture.Razor.Application.PurchaseOrders.DTOs;

namespace CleanArchitecture.Razor.Application.PurchaseOrders.Queries.GetAll
{
    public class GetAllPurchaseOrdersQuery : IRequest<IEnumerable<PurchaseOrderDto>>
    {
       
    }
    
    public class GetAllPurchaseOrdersQueryHandler :
         IRequestHandler<GetAllPurchaseOrdersQuery, IEnumerable<PurchaseOrderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllPurchaseOrdersQueryHandler> _localizer;

        public GetAllPurchaseOrdersQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllPurchaseOrdersQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<PurchaseOrderDto>> Handle(GetAllPurchaseOrdersQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.PurchaseOrders
                         .ProjectTo<PurchaseOrderDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

