// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Queries.Pagination;

    public class MigrationProjectsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<MigrationProjectDto>>
    {
       
    }
    
    public class MigrationProjectsWithPaginationQueryHandler :
         IRequestHandler<MigrationProjectsWithPaginationQuery, PaginatedData<MigrationProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<MigrationProjectsWithPaginationQueryHandler> _localizer;

        public MigrationProjectsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<MigrationProjectsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<MigrationProjectDto>> Handle(MigrationProjectsWithPaginationQuery request, CancellationToken cancellationToken)
        {
  
           var filters = PredicateBuilder.FromFilter<MigrationProject>(request.FilterRules);
           var data = await _context.MigrationProjects.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<MigrationProjectDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
   }