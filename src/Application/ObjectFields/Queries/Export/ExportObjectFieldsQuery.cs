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
                    { _localizer["Field Name"], item => item.Name },
                    { _localizer["Field Description"], item => item.Description }
           
                }
                , _localizer["DataElement"]);
            return result;
        }
    }