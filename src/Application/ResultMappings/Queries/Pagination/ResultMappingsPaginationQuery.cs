// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using System.Dynamic;
using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Queries.Pagination;

public class ResultMappingsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<ResultMappingDto>>
{

}
public class ResultMappingDataWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<ResultMappingDataDto>>
{
    public int Id { get; set; }
}


public class ResultMappingsWithPaginationQueryHandler :
     IRequestHandler<ResultMappingDataWithPaginationQuery, PaginatedData<ResultMappingDataDto>>,
     IRequestHandler<ResultMappingsWithPaginationQuery, PaginatedData<ResultMappingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ResultMappingsWithPaginationQueryHandler> _localizer;

    public ResultMappingsWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<ResultMappingsWithPaginationQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PaginatedData<ResultMappingDto>> Handle(ResultMappingsWithPaginationQuery request, CancellationToken cancellationToken)
    {

        var filters = PredicateBuilder.FromFilter<ResultMapping>(request.FilterRules);
        var data = await _context.ResultMappings.Where(filters)
             .OrderBy($"{request.Sort} {request.Order}")
             .ProjectTo<ResultMappingDto>(_mapper.ConfigurationProvider)
             .PaginatedDataAsync(request.Page, request.Rows);
        return data;
    }
    public async Task<PaginatedData<ResultMappingDataDto>> Handle(ResultMappingDataWithPaginationQuery request, CancellationToken cancellationToken)
    {

        var filters = PredicateBuilder.FromFilter<ResultMappingData>(request.FilterRules);
        var data = await _context.ResultMappingDatas.Where(x=>x.ResultMappingId==request.Id).Where(filters)
             .OrderBy($"{request.Sort} {request.Order}")
             .ProjectTo<ResultMappingDataDto>(_mapper.ConfigurationProvider)
             .PaginatedDataAsync(request.Page, request.Rows);
        return data;
    }

     
 
}