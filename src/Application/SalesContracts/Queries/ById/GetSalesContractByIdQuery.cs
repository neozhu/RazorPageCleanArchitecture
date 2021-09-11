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
    public class GetSalesContractByIdQuery : IRequest<SalesContractDto>
    {
       public int Id {  get; set; }
    }
    
    public class GetSalesContractByIdQueryHandler :
         IRequestHandler<GetSalesContractByIdQuery, SalesContractDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetSalesContractByIdQueryHandler> _localizer;

        public GetSalesContractByIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetSalesContractByIdQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<SalesContractDto> Handle(GetSalesContractByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.SalesContracts.Where(x => x.Id == request.Id)
                .Include(x => x.Customer).Include(x => x.Project)
                .FirstAsync(cancellationToken);
            return _mapper.Map<SalesContractDto>(data);
        }
    }
}

