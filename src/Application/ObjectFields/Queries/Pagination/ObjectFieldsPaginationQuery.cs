// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Queries.Pagination;

    public class ObjectFieldsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<ObjectFieldDto>>
    {
       
    }
    
    public class ObjectFieldsWithPaginationQueryHandler :
         IRequestHandler<ObjectFieldsWithPaginationQuery, PaginatedData<ObjectFieldDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ObjectFieldsWithPaginationQueryHandler> _localizer;

        public ObjectFieldsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<ObjectFieldsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<ObjectFieldDto>> Handle(ObjectFieldsWithPaginationQuery request, CancellationToken cancellationToken)
        {

           var filters = PredicateBuilder.FromFilter<ObjectField>(request.FilterRules);
           var data = await _context.ObjectFields.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<ObjectFieldDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
   }