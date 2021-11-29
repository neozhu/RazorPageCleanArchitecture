// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Queries.Export;

    public class ExportMigrationProjectsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportMigrationProjectsQueryHandler :
         IRequestHandler<ExportMigrationProjectsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportMigrationProjectsQueryHandler> _localizer;

        public ExportMigrationProjectsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportMigrationProjectsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportMigrationProjectsQuery request, CancellationToken cancellationToken)
        {
          
            var filters = PredicateBuilder.FromFilter<MigrationProject>(request.FilterRules);
            var data = await _context.MigrationProjects.Where(filters)
                       .OrderBy("{request.Sort} {request.Order}")
                       .ProjectTo<MigrationProjectDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<MigrationProjectDto, object>>()
                {
                    { _localizer["Project Name"], item => item.Name },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Begin Date"], item => item.BeginDateTime },
                    { _localizer["Finished Date"], item => item.FinishedDateTime },
                    { _localizer["Description"], item => item.Description },
                }
                , _localizer["MigrationProjects"]);
            return result;
        }
    }