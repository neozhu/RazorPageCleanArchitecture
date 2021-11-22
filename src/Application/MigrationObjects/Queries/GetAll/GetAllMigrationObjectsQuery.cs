// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Queries.GetAll;

    public class GetAllMigrationObjectsQuery : IRequest<IEnumerable<MigrationObjectDto>>
    {
       
    }
    
    public class GetAllMigrationObjectsQueryHandler :
         IRequestHandler<GetAllMigrationObjectsQuery, IEnumerable<MigrationObjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllMigrationObjectsQueryHandler> _localizer;

        public GetAllMigrationObjectsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllMigrationObjectsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<MigrationObjectDto>> Handle(GetAllMigrationObjectsQuery request, CancellationToken cancellationToken)
        {
  
            var data = await _context.MigrationObjects
                         .ProjectTo<MigrationObjectDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }

