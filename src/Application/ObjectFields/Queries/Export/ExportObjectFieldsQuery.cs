// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Queries.Export;

    public class ExportObjectFieldsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportObjectFieldsQueryHandler :
         IRequestHandler<ExportObjectFieldsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportObjectFieldsQueryHandler> _localizer;

        public ExportObjectFieldsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportObjectFieldsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportObjectFieldsQuery request, CancellationToken cancellationToken)
        {
 
            var filters = PredicateBuilder.FromFilter<ObjectField>(request.FilterRules);
            var data = await _context.ObjectFields.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<ObjectFieldDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<ObjectFieldDto, object>>()
                {
                    { _localizer["Field"], item => item.Name },
                    { _localizer["Field Description"], item => item.Description },
                    { _localizer["Master Data Relevant"], item => item.MasterDataRelevant },
                    { _localizer["Tech Mock Master Data"], item => item.TechMockMasterData },
                    { _localizer["Team"], item => item.Team },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Link"], item => item.Link },
                    { _localizer["Legacy System"], item => item.LegacySystem },
                    { _localizer["Is Used AK1"], item => item.IsUsedAK1 },
                    { _localizer["Major Table where object is used"], item => item.MajorTable },
                    { _localizer["Nr. of Cases where used"], item => item.Cases },
                    { _localizer["Numbers if different values"], item => item.Numbers },
                    { _localizer["Relevant Migration Objects"], item => item.RelevantObjects },
                    { _localizer["Check"], item => item.Check },
                    { _localizer["Comments"], item => item.Comments },
                }
                , _localizer["ObjectFields"]);
            return result;
        }
    }