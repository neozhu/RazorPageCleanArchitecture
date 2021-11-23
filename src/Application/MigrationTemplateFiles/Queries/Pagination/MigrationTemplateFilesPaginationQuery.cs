// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Queries.Pagination;

    public class MigrationTemplateFilesWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<MigrationTemplateFileDto>>
    {
       
    }
    
    public class MigrationTemplateFilesWithPaginationQueryHandler :
         IRequestHandler<MigrationTemplateFilesWithPaginationQuery, PaginatedData<MigrationTemplateFileDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<MigrationTemplateFilesWithPaginationQueryHandler> _localizer;

        public MigrationTemplateFilesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<MigrationTemplateFilesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<MigrationTemplateFileDto>> Handle(MigrationTemplateFilesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            
           var filters = PredicateBuilder.FromFilter<MigrationTemplateFile>(request.FilterRules);
           var data = await _context.MigrationTemplateFiles.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<MigrationTemplateFileDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
   }