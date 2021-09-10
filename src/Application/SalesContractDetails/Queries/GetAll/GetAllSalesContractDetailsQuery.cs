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

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Queries.GetAll
{
    public class GetAllSalesContractDetailsQuery : IRequest<IEnumerable<SalesContractDetailDto>>
    {
       
    }
    
    public class GetAllSalesContractDetailsQueryHandler :
         IRequestHandler<GetAllSalesContractDetailsQuery, IEnumerable<SalesContractDetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllSalesContractDetailsQueryHandler> _localizer;

        public GetAllSalesContractDetailsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllSalesContractDetailsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<SalesContractDetailDto>> Handle(GetAllSalesContractDetailsQuery request, CancellationToken cancellationToken)
        {
      
            var data = await _context.SalesContractDetails
                         .ProjectTo<SalesContractDetailDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

