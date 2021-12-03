// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Queries.Export;

public class ExportFieldMappingValuesQuery : IRequest<byte[]>
{
    public string FilterRules { get; set; }
    public string Sort { get; set; } = "Id";
    public string Order { get; set; } = "desc";
}

public class ExportFieldMmappingValuesDataQuery : IRequest<byte[]>
{
    public int MappingRuleId { get; set; }
}

public class ExportFieldMappingValuesQueryHandler :
     IRequestHandler<ExportFieldMmappingValuesDataQuery, byte[]>,
     IRequestHandler<ExportFieldMappingValuesQuery, byte[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IExcelService _excelService;
    private readonly IStringLocalizer<ExportFieldMappingValuesQueryHandler> _localizer;

    public ExportFieldMappingValuesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IExcelService excelService,
        IStringLocalizer<ExportFieldMappingValuesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _excelService = excelService;
        _localizer = localizer;
    }

    public async Task<byte[]> Handle(ExportFieldMappingValuesQuery request, CancellationToken cancellationToken)
    {
        var filters = PredicateBuilder.FromFilter<FieldMappingValue>(request.FilterRules);
        var data = await _context.FieldMappingValues.Where(filters)
                   .OrderBy($"{request.Sort} {request.Order}")
                   .ProjectTo<FieldMappingValueDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);
        var result = await _excelService.ExportAsync(data,
            new Dictionary<string, Func<FieldMappingValueDto, object>>()
            {
                    { _localizer["Mapping Rule Name"], item => item.MappingRule },
                    { _localizer["Mock"], item => item.Mock },
                    { _localizer["Legacy 1"], item => item.Legacy1 },
                    { _localizer["Legacy 2"], item => item.Legacy2 },
                    { _localizer["Legacy 3"], item => item.Legacy3 },
                    { _localizer["New Value"], item => item.NewValue },
                    { _localizer["Legacy System"], item => item.LegacySystem },
                    { _localizer["Comments"], item => item.Comments }
            }
            , _localizer["FieldMappingValues"]);
        return result;
    }

    public async Task<byte[]> Handle(ExportFieldMmappingValuesDataQuery request, CancellationToken cancellationToken)
    {
        var mappingrule = await _context.MappingRules.FirstAsync(x => x.Id == request.MappingRuleId);
        var dic = new Dictionary<string, Func<FieldMappingValueDto, object>>();
        if (mappingrule.IsMock)
        {
            dic.Add(_localizer["Mock"],item=>item.Mock);
        }
        if (!string.IsNullOrEmpty(mappingrule.LegacyField1))
        {
            dic.Add($"{mappingrule.LegacyField1}(Legacy)", item=>item.Legacy1);
        }
        if (!string.IsNullOrEmpty(mappingrule.LegacyField2))
        {
            dic.Add($"{mappingrule.LegacyField2}(Legacy)",item=>item.Legacy2);
        }
        if (!string.IsNullOrEmpty(mappingrule.LegacyField3))
        {
            dic.Add($"{mappingrule.LegacyField3}(Legacy)",item=>item.Legacy3);
        }
        if (!string.IsNullOrEmpty(mappingrule.NewValueField))
        {
            dic.Add($"{mappingrule.NewValueField}(New)",item=>item.NewValue);
        }
        dic.Add(_localizer["Comments"], item=>item.Comments);
        var data = await _context.FieldMappingValues.Where(x=>x.MappingRuleId==request.MappingRuleId)
                   .OrderBy(x=>x.Id)
                   .ProjectTo<FieldMappingValueDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);
        var result = await _excelService.ExportAsync(data,
            dic
            , "Data");
        return result;
    }
}