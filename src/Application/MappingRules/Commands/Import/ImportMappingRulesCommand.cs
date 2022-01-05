// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Create;
using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Import;

public class ImportMappingRulesCommand : IRequest<Result>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}
public class CreateMappingRulesTemplateCommand : IRequest<byte[]>
{
    public IEnumerable<string> Fields { get; set; }
    public string SheetName { get; set; }
}

public class ImportMappingRulesCommandHandler :
             IRequestHandler<CreateMappingRulesTemplateCommand, byte[]>,
             IRequestHandler<ImportMappingRulesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ImportMappingRulesCommandHandler> _logger;
    private readonly IStringLocalizer<ImportMappingRulesCommandHandler> _localizer;
    private readonly IValidator<CreateMappingRuleCommand> _validator;
    private readonly IExcelService _excelService;

    public ImportMappingRulesCommandHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IStringLocalizer<ImportMappingRulesCommandHandler> localizer,
        IValidator<CreateMappingRuleCommand> validator,
        IMapper mapper,
        ILogger<ImportMappingRulesCommandHandler> logger
        )
    {
        _context = context;
        _localizer = localizer;
        _validator = validator;
        _excelService = excelService;
        _mapper = mapper;
        _logger = logger;
    }
    //public async Task<Result> Handle(ImportMappingRulesCommand request, CancellationToken cancellationToken)
    //{
    //    var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, TempFieldDto, object>>
    //        {
    //            { _localizer["Field Name"], (row,item) => item.Name = row[_localizer["Field Name"]]?.ToString() },
    //            { _localizer["Field Description"], (row,item) => item.Description = row[_localizer["Field Description"]]?.ToString() },
    //            { _localizer["Associated Type"], (row,item) => item.AssociatedType = row[_localizer["Associated Type"]]?.ToString() },
    //            //{ _localizer["Date Type"], (row,item) => item.DateType = row[_localizer["DateType"]]?.ToString() },
    //            { _localizer["Length"], (row,item) => item.Length =row.IsNull(_localizer["Length"])?null: Convert.ToInt32(row[_localizer["Length"]]?.ToString()) },
    //            { _localizer["Direct"], (row,item) => item.Direct = row[_localizer["Direct"]]?.ToString() },
    //            { _localizer["Parameter Name"], (row,item) => item.ParameterName = row[_localizer["Parameter Name"]]?.ToString() },
    //            { _localizer["Title"], (row,item) => item.Title = row[_localizer["Title"]]?.ToString() },
    //            { _localizer["Source Template Name"], (row,item) => item.SourceTemplateName = row[_localizer["Source Template Name"]]?.ToString() },
    //        }, _localizer["ObjectFields"]);
    //    if (result.Succeeded)
    //    {
    //        var importItems = result.Data;
    //        var errors = new List<string>();
    //        var errorsOccurred = false;
    //        var group=importItems.GroupBy(x => x.SourceTemplateName);
    //        foreach(var g in group)
    //        {
    //            var array = g.ToArray();
    //            Array.Reverse(array);
    //            var exportfield = array.Where(x => x.Direct == "Export Parameter").FirstOrDefault();
    //            if(exportfield==null) continue;
    //            var importfields = array.Where(x => x.Direct == "Import Parameter").ToList();
    //            var exportfielddesc = await _context.ObjectFields.Where(x => x.Name == exportfield.Name).FirstOrDefaultAsync();
    //            var defafultproject = await _context.MigrationProjects.FirstAsync();
    //            var item = new MappingRule()
    //            {
    //                Name = g.Key.Remove(g.Key.LastIndexOf(".")),
    //                MigrationProjectId = defafultproject.Id,
    //                TemplateFile = "Files/TemplateFiles/" + g.Key,
    //                Status = "Not started",
    //                Comments = array[0].Title,
    //                NewValueField = exportfield.Name,
    //                ExportParameterField = exportfield.ParameterName,
    //                NewValueFieldDescription = exportfielddesc?.Description,
    //                TemplateDescription = array[0].Title
    //            };
    //            var index = 0;
    //            foreach(var field in importfields)
    //            {
    //                var importfielddesc = await _context.ObjectFields.Where(x => x.Name == field.Name).FirstOrDefaultAsync();
    //                ++index ;
    //                switch (index) {
    //                    case 1:
    //                        item.LegacyField1 = field.Name;
    //                        item.ImportParameterField1 = field.ParameterName;
    //                        item.LegacyDescription1 = importfielddesc?.Description;
    //                        break;
    //                    case 2:
    //                        item.LegacyField2 = field.Name;
    //                        item.ImportParameterField2 = field.ParameterName;
    //                        item.LegacyDescription2 = importfielddesc?.Description;
    //                        break;
    //                    case 3:
    //                        item.LegacyField3 = field.Name;
    //                        item.ImportParameterField3 = field.ParameterName;
    //                        item.LegacyDescription3 = importfielddesc?.Description;
    //                        break;
    //                   default:
    //                        item.LegacyField4 = field.Name;
    //                        item.ImportParameterField4 = field.ParameterName;
    //                        item.LegacyDescription4 = importfielddesc?.Description;
    //                        break;
    //                }
    //            }

    //           await _context.MappingRules.AddAsync(item);
    //        }
    //        await _context.SaveChangesAsync(cancellationToken);


    //        if (errorsOccurred)
    //        {
    //            return await Result.FailureAsync(errors);
    //        }

    //        await _context.SaveChangesAsync(cancellationToken);
    //        return await Result.SuccessAsync();
    //    }
    //    else
    //    {
    //        return await Result.FailureAsync(result.Errors);
    //    }

    //}


    //public async Task<Result> Handle(ImportMappingRulesCommand request, CancellationToken cancellationToken)
    //{
    //    var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MappingRule, object>>
    //        {
    //            { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },
    //            { _localizer["Status"], (row,item) => item.Status = row[_localizer["Status"]]?.ToString() },
    //            { _localizer["Active"], (row, item) => item.Active = row[_localizer["Active"]]?.ToString() },
    //            { _localizer["ImportParameterField1"], (row,item) => item.ImportParameterField1 = row[_localizer["ImportParameterField1"]]?.ToString() },
    //            { _localizer["LegacyField1"], (row,item) => item.LegacyField1 = row[_localizer["LegacyField1"]]?.ToString() },
    //            { _localizer["LegacyDescription1"], (row,item) => item.LegacyDescription1 = row[_localizer["LegacyDescription1"]]?.ToString() },
    //            { _localizer["ImportParameterField2"], (row,item) => item.ImportParameterField2 = row[_localizer["ImportParameterField2"]]?.ToString() },
    //            { _localizer["LegacyField2"], (row,item) => item.LegacyField2 = row[_localizer["LegacyField2"]]?.ToString() },
    //            { _localizer["LegacyDescription2"], (row,item) => item.LegacyDescription2 = row[_localizer["LegacyDescription2"]]?.ToString() },
    //            { _localizer["ImportParameterField3"], (row,item) => item.ImportParameterField3 = row[_localizer["ImportParameterField3"]]?.ToString() },
    //            { _localizer["LegacyField3"], (row,item) => item.LegacyField3 = row[_localizer["LegacyField3"]]?.ToString() },
    //            { _localizer["LegacyDescription3"], (row,item) => item.LegacyDescription3 = row[_localizer["LegacyDescription3"]]?.ToString() },
    //            { _localizer["ExportParameterField"], (row,item) => item.ExportParameterField = row[_localizer["ExportParameterField"]]?.ToString() },
    //            { _localizer["NewValueField"], (row,item) => item.NewValueField = row[_localizer["NewValueField"]]?.ToString() },
    //            { _localizer["NewValueFieldDescription"], (row,item) => item.NewValueFieldDescription = row[_localizer["NewValueFieldDescription"]]?.ToString() },
    //            { _localizer["Team"], (row,item) => item.Team = row[_localizer["Team"]]?.ToString() },
    //            { _localizer["LegacySystem"], (row,item) => item.LegacySystem = row[_localizer["LegacySystem"]]?.ToString() },
    //            { _localizer["ProjectName"], (row,item) => item.ProjectName = row[_localizer["ProjectName"]]?.ToString() },
    //            { _localizer["RelevantObjects"], (row,item) => item.RelevantObjects = row[_localizer["RelevantObjects"]]?.ToString() },
    //            { _localizer["Comments"], (row,item) => item.Comments = row[_localizer["Comments"]]?.ToString() },
    //            { _localizer["TemplateFile"], (row,item) => item.TemplateFile = row[_localizer["TemplateFile"]]?.ToString() },
    //            { _localizer["TemplateDescription"], (row,item) => item.TemplateDescription = row[_localizer["TemplateDescription"]]?.ToString() },
    //            { _localizer["MigrationApproach"], (row,item) => item.MigrationApproach = row[_localizer["MigrationApproach"]]?.ToString() },
    //            { _localizer["ObjectName"], (row,item) => item.ObjectName = row[_localizer["ObjectName"]]?.ToString() },

    //        }, _localizer["MappingRules"]);
    //    if (result.Succeeded)
    //    {
    //        var importItems = result.Data;
    //        foreach (var item in importItems)
    //        {
    //            var exist = await _context.MappingRules.AnyAsync(x => x.Name == item.Name
    //                            , cancellationToken);
    //            if (!exist)
    //            {
    //                await _context.MappingRules.AddAsync(item, cancellationToken);
    //            }

    //        }
    //        await _context.SaveChangesAsync(cancellationToken);
    //        return await Result.SuccessAsync();
    //    }
    //    else
    //    {
    //        return await Result.FailureAsync(result.Errors);
    //    }
    //}

    public async Task<byte[]> Handle(CreateMappingRulesTemplateCommand request, CancellationToken cancellationToken)
    {

        var fields = new string[] {
                   _localizer["Name"],
                   _localizer["Status"],
                   _localizer["Active"],
                   _localizer["ImportParameterFieldName1"],
                   _localizer["LegacyFieldName1"],
                   _localizer["LegacyFieldDescription1"],
                   _localizer["ImportParameterFieldName2"],
                   _localizer["LegacyFieldName2"],
                   _localizer["LegacyFieldDescription2"],
                   _localizer["ImportParameterFieldName3"],
                   _localizer["LegacyFieldName3"],
                   _localizer["LegacyFieldDescription3"],
                   _localizer["ExportParameterField"],
                   _localizer["NewFieldName"],
                   _localizer["NewFieldDescription"],
                   _localizer["MigrationApproach"],
                   _localizer["Team"],
                   _localizer["LegacySystem"],
                   _localizer["ProjectName"],
                   _localizer["RelevantObjects"],
                   _localizer["Comments"],
                   _localizer["ObjectName"],
                   _localizer["TemplateFile"],
                   _localizer["TemplateDescription"],
            };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["MappingRules"]);
        return result;
    }

    public async Task<Result> Handle(ImportMappingRulesCommand request, CancellationToken cancellationToken)
    {
        var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, FieldMappingValueDto, object>>
            {
                { _localizer["Matched Rule Name"], (row,item) => item.ObjectName = row[_localizer["Matched Rule Name"]]?.ToString() },
                { _localizer["Confirmed Approach"], (row,item) => item.MigrationApproach = row[_localizer["Confirmed Approach"]]?.ToString() },
                { _localizer["New"], (row, item) => item.NewValue = row[_localizer["New"]]?.ToString() },
                { _localizer["Legacy1"], (row,item) => item.Legacy1 = row[_localizer["Legacy1"]]?.ToString() },
                { _localizer["Legacy2"], (row,item) => item.Legacy2 = row[_localizer["Legacy2"]]?.ToString() },
                { _localizer["Legacy3"], (row,item) => item.Legacy3 = row[_localizer["Legacy3"]]?.ToString() },
             }, _localizer["Sheet1"]);
        if (result.Succeeded) {
            var items = result.Data;
            var group = items.GroupBy(x => new { x.ObjectName, x.MigrationApproach })
                             .Select(g => new { g.Key.MigrationApproach, g.Key.ObjectName, Count = g.Count() })
                             .ToList();
            foreach(var g in group)
            {
                if (g.Count > 1)
                {
                    var rule = await _context.MappingRules.Where(x => x.ObjectName == g.ObjectName
                                                               && x.MigrationApproach == g.MigrationApproach)
                                                        .FirstOrDefaultAsync();
                    if (rule != null)
                    {
                        var exist = await _context.FieldMappingValues.AnyAsync(x => x.MappingRuleId == rule.Id);
                        if (exist)
                        {
                            _logger.LogWarning($"{g.ObjectName}-{g.MigrationApproach}:has maintained value");
                            continue;
                        }
                        foreach (var item in items.Where(x => x.MigrationApproach == g.MigrationApproach && x.ObjectName == g.ObjectName).ToList())
                        {
                            var mappingvalue = new FieldMappingValue()
                            {
                                MappingRuleId = rule.Id,
                                Legacy1 = item.Legacy1,
                                Legacy2 = item.Legacy2,
                                Legacy3 = item.Legacy3,
                                Legacy4 = item.Legacy4,
                                NewValue = item.NewValue,
                            };

                            await _context.FieldMappingValues.AddAsync(mappingvalue,cancellationToken);
                          
                        }
                        rule.Active = "Active";
                        _context.MappingRules.Update(rule);
                    }
                    else
                    {
                        _logger.LogWarning($"{g.ObjectName}-{g.MigrationApproach}:not found maping rule");
                    }
                }
                else
                {
                    var rule = await _context.MappingRules.Where(x => x.ObjectName == g.ObjectName
                                                              && x.MigrationApproach == g.MigrationApproach)
                                                       .FirstOrDefaultAsync();
                    if (rule != null)
                    {
                        var exist = await _context.FieldMappingValues.AnyAsync(x => x.MappingRuleId == rule.Id);
                        if (exist)
                        {
                            _logger.LogWarning($"{g.ObjectName}-{g.MigrationApproach}:has maintained value");
                            continue;
                        }

                        var item = items.Where(x => x.MigrationApproach == g.MigrationApproach && x.ObjectName == g.ObjectName).FirstOrDefault();
                        if (!string.IsNullOrEmpty(item.Legacy1))
                        {
                            var mappingvalue = new FieldMappingValue()
                            {
                                MappingRuleId = rule.Id,
                                Legacy1 = item.Legacy1,
                                Legacy2 = item.Legacy2,
                                Legacy3 = item.Legacy3,
                                Legacy4 = item.Legacy4,
                                NewValue = item.NewValue,
                            };

                            await _context.FieldMappingValues.AddAsync(mappingvalue, cancellationToken);
                            rule.Active = "Active";
                            _context.MappingRules.Update(rule);
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"{g.ObjectName}-{g.MigrationApproach}:not found maping rule");
                    }
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        else {
            return await Result.FailureAsync(result.Errors);
        }
    }
}

