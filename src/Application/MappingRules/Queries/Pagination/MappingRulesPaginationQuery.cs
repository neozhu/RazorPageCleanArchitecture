// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Queries.Pagination;

public class MappingRulesWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<MappingRuleDto>>
{

}

public class MappingRulesWithPaginationQueryHandler :
     IRequestHandler<MappingRulesWithPaginationQuery, PaginatedData<MappingRuleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<MappingRulesWithPaginationQueryHandler> _localizer;

    public MappingRulesWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<MappingRulesWithPaginationQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PaginatedData<MappingRuleDto>> Handle(MappingRulesWithPaginationQuery request, CancellationToken cancellationToken)
    {
       
        var filters = PredicateBuilder.FromFilter<MappingRule>(request.FilterRules);
 
        var data = await _context.MappingRules.Where(filters)
             .OrderBy($"{request.Sort} {request.Order}")
             .ProjectTo<MappingRuleDto>(_mapper.ConfigurationProvider)
             .PaginatedDataAsync(request.Page, request.Rows);
        return data;
    }
}