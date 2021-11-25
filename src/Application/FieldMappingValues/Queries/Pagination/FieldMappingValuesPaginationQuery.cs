// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Queries.Pagination;

    public class FieldMappingValuesWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<FieldMappingValueDto>>
    {
       
    }
    
    public class FieldMappingValuesWithPaginationQueryHandler :
         IRequestHandler<FieldMappingValuesWithPaginationQuery, PaginatedData<FieldMappingValueDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<FieldMappingValuesWithPaginationQueryHandler> _localizer;

        public FieldMappingValuesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<FieldMappingValuesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<FieldMappingValueDto>> Handle(FieldMappingValuesWithPaginationQuery request, CancellationToken cancellationToken)
        {
    
           var filters = PredicateBuilder.FromFilter<FieldMappingValue>(request.FilterRules);
           var data = await _context.FieldMappingValues.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<FieldMappingValueDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
   }