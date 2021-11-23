// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Queries.Export;

    public class ExportFieldValueMappingsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportFieldValueMappingsQueryHandler :
         IRequestHandler<ExportFieldValueMappingsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportFieldValueMappingsQueryHandler> _localizer;

        public ExportFieldValueMappingsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportFieldValueMappingsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportFieldValueMappingsQuery request, CancellationToken cancellationToken)
        {
           
            var filters = PredicateBuilder.FromFilter<FieldValueMapping>(request.FilterRules);
            var data = await _context.FieldValueMappings.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<FieldValueMappingDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<FieldValueMappingDto, object>>()
                {
                   { _localizer["Field"], item => item.FieldName },
                   { _localizer["Field Description"], item => item.Description },
                   { _localizer["Target System"], item => item.Target },
                   { _localizer["Legacy 1"], item => item.Legacy1 },
                   { _localizer["Legacy 2"], item => item.Legacy2 },
                   { _localizer["Legacy 3"], item => item.Legacy3 },
                   { _localizer["New"], item => item.NewValue },
                   { _localizer["Text Legacy System"], item => item.LegacySystem},
                   { _localizer["Comments"], item => item.Comments }

                }
                , _localizer["FieldValueMappings"]);
            return result;
        }
    }