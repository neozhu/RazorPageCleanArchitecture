// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Queries.GetAll;

    public class GetAllMigrationTemplateFilesQuery : IRequest<IEnumerable<MigrationTemplateFileDto>>
    {
       
    }
    
    public class GetAllMigrationTemplateFilesQueryHandler :
         IRequestHandler<GetAllMigrationTemplateFilesQuery, IEnumerable<MigrationTemplateFileDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllMigrationTemplateFilesQueryHandler> _localizer;

        public GetAllMigrationTemplateFilesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllMigrationTemplateFilesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<MigrationTemplateFileDto>> Handle(GetAllMigrationTemplateFilesQuery request, CancellationToken cancellationToken)
        {
            
            var data = await _context.MigrationTemplateFiles
                         .ProjectTo<MigrationTemplateFileDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


