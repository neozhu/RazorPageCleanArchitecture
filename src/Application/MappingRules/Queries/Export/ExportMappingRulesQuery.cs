// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Queries.Export;

public class ExportMappingRulesQuery : IRequest<byte[]>
{
    public string FilterRules { get; set; }
    public string Sort { get; set; } = "Id";
    public string Order { get; set; } = "desc";
}

public class ExportMappingRulesQueryHandler :
     IRequestHandler<ExportMappingRulesQuery, byte[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IExcelService _excelService;
    private readonly IStringLocalizer<ExportMappingRulesQueryHandler> _localizer;

    public ExportMappingRulesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IExcelService excelService,
        IStringLocalizer<ExportMappingRulesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _excelService = excelService;
        _localizer = localizer;
    }

    public async Task<byte[]> Handle(ExportMappingRulesQuery request, CancellationToken cancellationToken)
    {

        var filters = PredicateBuilder.FromFilter<MappingRule>(request.FilterRules);
        var data = await _context.MappingRules.Where(filters)
                   .OrderBy($"{request.Sort} {request.Order}")
                   .ProjectTo<MappingRuleDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);
        var result = await _excelService.ExportAsync(data,
            new Dictionary<string, Func<MappingRuleDto, object>>()
            {
                    { _localizer["Mapping Rule Name"], item => item.Name },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Legacy Field Name 1"], item => item.LegacyField1 },
                    { _localizer["Import Parameter Field Name 1"], item => item.ImportParameterField1 },
                    { _localizer["Legacy Field Description 1"], item => item.LegacyDescription1 },
                    { _localizer["Legacy Field Name 2"], item => item.LegacyField2 },
                    { _localizer["Import Parameter Field Name 2"], item => item.ImportParameterField2 },
                    { _localizer["Legacy Field Description 2"], item => item.LegacyDescription2 },
                    { _localizer["Legacy Field Name 3"], item => item.LegacyField3 },
                    { _localizer["Import Parameter Field Name 3"], item => item.ImportParameterField3 },
                    { _localizer["Legacy Field Description 3"], item => item.LegacyDescription3 },
                    { _localizer["New Field Name"], item => item.NewValueField },
                    { _localizer["Export Parameter Field"], item => item.ExportParameterField },
                    { _localizer["New Field Description"], item => item.NewValueFieldDescription },
                    { _localizer["Template File"], item => item.TemplateFile },
                    { _localizer["Template File Description"], item => item.TemplateDescription },
                    { _localizer["Is Mock"], item => item.IsMock },
                    { _localizer["Team"], item => item.Team },
                    { _localizer["Legacy System"], item => item.LegacySystem },
                    { _localizer["Project Name"], item => item.ProjectName },
                    { _localizer["Relevant Objects"], item => item.RelevantObjects },
                    { _localizer["Comments"], item => item.Comments },
                    { _localizer["Created By"], item => item.CreatedBy },
                    { _localizer["Last Modified By"], item => item.LastModifiedBy }
            }
            , _localizer["MappingRules"]);
        return result;
    }
}