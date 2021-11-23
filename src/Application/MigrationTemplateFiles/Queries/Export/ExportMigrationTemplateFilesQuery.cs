// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Queries.Export;

    public class ExportMigrationTemplateFilesQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportMigrationTemplateFilesQueryHandler :
         IRequestHandler<ExportMigrationTemplateFilesQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportMigrationTemplateFilesQueryHandler> _localizer;

        public ExportMigrationTemplateFilesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportMigrationTemplateFilesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportMigrationTemplateFilesQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ExportMigrationTemplateFilesQueryHandler method 
            var filters = PredicateBuilder.FromFilter<MigrationTemplateFile>(request.FilterRules);
            var data = await _context.MigrationTemplateFiles.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<MigrationTemplateFileDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<MigrationTemplateFileDto, object>>()
                {
                   { _localizer["Migration Template Name"], item => item.Name },
                   { _localizer["Description"], item => item.Description },
                   { _localizer["Object Field"], item => item.ObjectField },
                   { _localizer["Import Field 1"], item => item.Legacy1Field },
                   { _localizer["Import Field 2"], item => item.Legacy2Field },
                   { _localizer["Import Field 3"], item => item.Legacy3Field },
                   { _localizer["Export Field"], item => item.NewValueField },
                   { _localizer["FilePath"], item => item.FilePath },
                   { _localizer["Legacy System"], item => item.LegacySystem },
                   { _localizer["Comments"], item => item.Comments }
                }
                , _localizer["MigrationTemplateFiles"]);
            return result;
        }
    }