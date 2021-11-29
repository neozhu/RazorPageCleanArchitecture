// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Queries.GetAll;

    public class GetAllMigrationProjectsQuery : IRequest<IEnumerable<MigrationProjectDto>>
    {
       
    }
    
    public class GetAllMigrationProjectsQueryHandler :
         IRequestHandler<GetAllMigrationProjectsQuery, IEnumerable<MigrationProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllMigrationProjectsQueryHandler> _localizer;

        public GetAllMigrationProjectsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllMigrationProjectsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<MigrationProjectDto>> Handle(GetAllMigrationProjectsQuery request, CancellationToken cancellationToken)
        {

            var data = await _context.MigrationProjects
                         .ProjectTo<MigrationProjectDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


