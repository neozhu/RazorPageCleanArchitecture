// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Queries.Pagination;

    public class FieldValueMappingsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<FieldValueMappingDto>>
    {
       
    }
    
    public class FieldValueMappingsWithPaginationQueryHandler :
         IRequestHandler<FieldValueMappingsWithPaginationQuery, PaginatedData<FieldValueMappingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<FieldValueMappingsWithPaginationQueryHandler> _localizer;

        public FieldValueMappingsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<FieldValueMappingsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<FieldValueMappingDto>> Handle(FieldValueMappingsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing FieldValueMappingsWithPaginationQueryHandler method 
           var filters = PredicateBuilder.FromFilter<FieldValueMapping>(request.FilterRules);
           var data = await _context.FieldValueMappings.Where(filters)
                .OrderBy("{request.Sort} {request.Order}")
                .ProjectTo<FieldValueMappingDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
   }