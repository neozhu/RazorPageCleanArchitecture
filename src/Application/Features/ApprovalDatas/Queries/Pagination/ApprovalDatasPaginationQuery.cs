using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Pagination
{
    public class ApprovalDatasWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<ApprovalDataDto>>
    {
       
    }
    public class ApprovalHistoriesWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<ApprovalDataDto>>
    {

    }

    public class ApprovalDatasWithPaginationQueryHandler :
          IRequestHandler<ApprovalHistoriesWithPaginationQuery, PaginatedData<ApprovalDataDto>>,
         IRequestHandler<ApprovalDatasWithPaginationQuery, PaginatedData<ApprovalDataDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ApprovalDatasWithPaginationQueryHandler> _localizer;

        public ApprovalDatasWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<ApprovalDatasWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<ApprovalDataDto>> Handle(ApprovalDatasWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<ApprovalData>(request.FilterRules);
            var data = await _context.ApprovalDatas
                .Where(x=>x.Status=="Pending")
                .Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<ApprovalDataDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);

            return data;
        }
        public async Task<PaginatedData<ApprovalDataDto>> Handle(ApprovalHistoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<ApprovalData>(request.FilterRules);
            var data = await _context.ApprovalDatas
                .Where(x => x.Status == "Finished")
                .Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<ApprovalDataDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);

            return data;
        }
    }
}

