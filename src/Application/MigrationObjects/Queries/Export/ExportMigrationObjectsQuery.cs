// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Queries.Export;

    public class ExportMigrationObjectsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportMigrationObjectsQueryHandler :
         IRequestHandler<ExportMigrationObjectsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportMigrationObjectsQueryHandler> _localizer;

        public ExportMigrationObjectsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportMigrationObjectsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportMigrationObjectsQuery request, CancellationToken cancellationToken)
        {
         
            var filters = PredicateBuilder.FromFilter<MigrationObject>(request.FilterRules);
            var data = await _context.MigrationObjects.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<MigrationObjectDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<MigrationObjectDto, object>>()
                {
                    { _localizer["Object Name"], item => item.Name },
                    { _localizer["Team"], item => item.Team },
                    { _localizer["Description"], item => item.Description },
                }
                , _localizer["MigrationObjects"]);
            return result;
        }
    }