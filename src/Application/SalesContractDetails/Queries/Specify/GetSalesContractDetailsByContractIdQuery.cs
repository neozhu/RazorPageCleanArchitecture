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

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Queries.Specify
{
    public class GetSalesContractDetailsByContractIdQuery : IRequest<IEnumerable<SalesContractDetailDto>>
    {
     public int SalesContractId { get; set; }
    }
    
    public class GetSalesContractDetailsByContractIdQueryHandler :
         IRequestHandler<GetSalesContractDetailsByContractIdQuery, IEnumerable<SalesContractDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetSalesContractDetailsByContractIdQueryHandler> _localizer;

        public GetSalesContractDetailsByContractIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetSalesContractDetailsByContractIdQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<SalesContractDetailDto>> Handle(GetSalesContractDetailsByContractIdQuery request, CancellationToken cancellationToken)
        {
     
            var data = await _context.SalesContractDetails
                        .Where(x=>x.SalesContractId==request.SalesContractId)
                        .Include(x=>x.SalesContract)
                         .ProjectTo<SalesContractDetailDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

