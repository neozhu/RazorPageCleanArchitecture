// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Queries.Pagination;

    public class MigrationObjectsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<MigrationObjectDto>>
    {
       
    }
    
    public class MigrationObjectsWithPaginationQueryHandler :
         IRequestHandler<MigrationObjectsWithPaginationQuery, PaginatedData<MigrationObjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<MigrationObjectsWithPaginationQueryHandler> _localizer;

        public MigrationObjectsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<MigrationObjectsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<MigrationObjectDto>> Handle(MigrationObjectsWithPaginationQuery request, CancellationToken cancellationToken)
        {
          
           var filters = PredicateBuilder.FromFilter<MigrationObject>(request.FilterRules);
           var data = await _context.MigrationObjects.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<MigrationObjectDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
   }