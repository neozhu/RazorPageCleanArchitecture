// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
    private readonly IStringLocalizer<ImportMappingRulesCommandHandler> _localizer;
    private readonly IValidator<CreateMappingRuleCommand> _validator;
    private readonly IExcelService _excelService;

    public ImportMappingRulesCommandHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IStringLocalizer<ImportMappingRulesCommandHandler> localizer,
        IValidator<CreateMappingRuleCommand> validator,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _validator = validator;
        _excelService = excelService;
        _mapper = mapper;
    }
    public async Task<Result> Handle(ImportMappingRulesCommand request, CancellationToken cancellationToken)
    {
        var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, TempFieldDto, object>>
            {
                { _localizer["Field Name"], (row,item) => item.Name = row[_localizer["Field Name"]]?.ToString() },
                { _localizer["Field Description"], (row,item) => item.Description = row[_localizer["Field Description"]]?.ToString() },
                { _localizer["Associated Type"], (row,item) => item.AssociatedType = row[_localizer["Associated Type"]]?.ToString() },
                //{ _localizer["Date Type"], (row,item) => item.DateType = row[_localizer["DateType"]]?.ToString() },
                { _localizer["Length"], (row,item) => item.Length =row.IsNull(_localizer["Length"])?null: Convert.ToInt32(row[_localizer["Length"]]?.ToString()) },
                { _localizer["Direct"], (row,item) => item.Direct = row[_localizer["Direct"]]?.ToString() },
                { _localizer["Parameter Name"], (row,item) => item.ParameterName = row[_localizer["Parameter Name"]]?.ToString() },
                { _localizer["Title"], (row,item) => item.Title = row[_localizer["Title"]]?.ToString() },
                { _localizer["Source Template Name"], (row,item) => item.SourceTemplateName = row[_localizer["Source Template Name"]]?.ToString() },
            }, _localizer["ObjectFields"]);
        if (result.Succeeded)
        {
            var importItems = result.Data;
            var errors = new List<string>();
            var errorsOccurred = false;
            var group=importItems.GroupBy(x => x.SourceTemplateName);
            foreach(var g in group)
            {
                var array = g.ToArray();
                Array.Reverse(array);
                var exportfield = array.Where(x => x.Direct == "Export Parameter").FirstOrDefault();
                if(exportfield==null) continue;
                var importfields = array.Where(x => x.Direct == "Import Parameter").ToList();
                var exportfielddesc = await _context.ObjectFields.Where(x => x.Name == exportfield.Name).FirstOrDefaultAsync();
                var defafultproject = await _context.MigrationProjects.FirstAsync();
                var item = new MappingRule()
                {
                    Name = g.Key.Remove(g.Key.LastIndexOf(".")),
                    MigrationProjectId = defafultproject.Id,
                    TemplateFile = "Files/TemplateFiles/" + g.Key,
                    Status = "Ongoing",
                    Comments = array[0].Title,
                    NewValueField = exportfield.Name,
                    ExportParameterField = exportfield.ParameterName,
                    NewValueFieldDescription = exportfielddesc?.Description,
                    TemplateDescription = array[0].Title
                };
                var index = 0;
                foreach(var field in importfields)
                {
                    var importfielddesc = await _context.ObjectFields.Where(x => x.Name == field.Name).FirstOrDefaultAsync();
                    ++index ;
                    switch (index) {
                        case 1:
                            item.LegacyField1 = field.Name;
                            item.ImportParameterField1 = field.ParameterName;
                            item.LegacyDescription1 = importfielddesc?.Description;
                            break;
                        case 2:
                            item.LegacyField2 = field.Name;
                            item.ImportParameterField2 = field.ParameterName;
                            item.LegacyDescription2 = importfielddesc?.Description;
                            break;
                        case 3:
                            item.LegacyField3 = field.Name;
                            item.ImportParameterField3 = field.ParameterName;
                            item.LegacyDescription3 = importfielddesc?.Description;
                            break;
                       default:
                            item.LegacyField4 = field.Name;
                            item.ImportParameterField4 = field.ParameterName;
                            item.LegacyDescription4 = importfielddesc?.Description;
                            break;
                    }
                }

               await _context.MappingRules.AddAsync(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
         

            if (errorsOccurred)
            {
                return await Result.FailureAsync(errors);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return await Result.SuccessAsync();
        }
        else
        {
            return await Result.FailureAsync(result.Errors);
        }


    }

    //private async Task<Result> NewMethod(ImportMappingRulesCommand request, object result, CancellationToken cancellationToken)
    //{
    //    var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MappingRule, object>>
    //        {
    //            { _localizer["Mapping Rule Name"], (row,item) => item.Name = row[_localizer["Mapping Rule Name"]]?.ToString() },
    //            { _localizer["Active"], (row, item) => item.Active = row[_localizer["Active"]]?.ToString() },
    //            { _localizer["Legacy Field Name 1"], (row,item) => item.LegacyField1 = row[_localizer["Legacy Field Name 1"]]?.ToString() },
    //            { _localizer["Legacy Field Description 1"], (row,item) => item.LegacyDescription1 = row[_localizer["Legacy Field Description 1"]]?.ToString() },
    //            { _localizer["Legacy Field Name 2"], (row,item) => item.LegacyField2 = row[_localizer["Legacy Field Name 2"]]?.ToString() },
    //            { _localizer["Legacy Field Description 2"], (row,item) => item.LegacyDescription2 = row[_localizer["Legacy Field Description 2"]]?.ToString() },
    //            { _localizer["Legacy Field Name 3"], (row,item) => item.LegacyField3 = row[_localizer["Legacy Field Name 3"]]?.ToString() },
    //            { _localizer["Legacy Field Description 3"], (row,item) => item.LegacyDescription3 = row[_localizer["Legacy Field Description 3"]]?.ToString() },
    //            { _localizer["New Field Name"], (row,item) => item.NewValueField = row[_localizer["New Field Name"]]?.ToString() },
    //            { _localizer["New Field Description"], (row,item) => item.NewValueFieldDescription = row[_localizer["New Field Description"]]?.ToString() },
    //            { _localizer["Is Mock"], (row,item) => item.IsMock =row.IsNull(_localizer["Is Mock"])?false:Convert.ToBoolean(row[_localizer["New Field Description"]]) },
    //            { _localizer["Team"], (row,item) => item.Team = row[_localizer["Team"]]?.ToString() },
    //            { _localizer["Legacy System"], (row,item) => item.LegacySystem = row[_localizer["Legacy System"]]?.ToString() },
    //            { _localizer["Project Name"], (row,item) => item.ProjectName = row[_localizer["Project Name"]]?.ToString() },
    //            { _localizer["Relevant Objects"], (row,item) => item.RelevantObjects = row[_localizer["Relevant Objects"]]?.ToString() },
    //            { _localizer["Comments"], (row,item) => item.Name = row[_localizer["Comments"]]?.ToString() },

    //        }, _localizer["MappingRules"]);
    //    if (result.Succeeded)
    //    {
    //        var importItems = result.Data;
    //        var errors = new List<string>();
    //        var errorsOccurred = false;
    //        foreach (var item in importItems)
    //        {
    //            var validationResult = await _validator.ValidateAsync(_mapper.Map<CreateMappingRuleCommand>(item), cancellationToken);
    //            if (validationResult.IsValid)
    //            {
    //                var field2 = item.LegacyField2 == String.Empty ? null : item.LegacyField2;
    //                var field3 = item.LegacyField3 == String.Empty ? null : item.LegacyField3;
    //                var exist = await _context.MappingRules.FirstOrDefaultAsync(x =>
    //                          x.LegacyField1 == item.LegacyField1
    //                       && x.NewValueField == item.NewValueField
    //                       && x.LegacyField2 == item.LegacyField2
    //                       && x.LegacyField3 == item.LegacyField3

    //                       , cancellationToken);
    //                if (exist == null)
    //                {
    //                    //var newitem = _mapper.Map<MappingRule>(item);
    //                    item.Status = "Not Started";
    //                    await _context.MappingRules.AddAsync(item, cancellationToken);
    //                }
    //                else
    //                {
    //                    exist.Name = item.Name;
    //                    exist.Comments = item.Comments;
    //                    exist.Team = item.Team;
    //                    exist.LegacySystem = item.LegacySystem;
    //                    exist.ProjectName = item.ProjectName;
    //                    exist.RelevantObjects = item.RelevantObjects;
    //                    exist.LegacyDescription1 = item.LegacyDescription1;
    //                    exist.LegacyDescription2 = item.LegacyDescription2;
    //                    exist.LegacyDescription3 = item.LegacyDescription3;
    //                    exist.NewValueFieldDescription = item.NewValueFieldDescription;
    //                    _context.MappingRules.Update(exist);
    //                }
    //            }
    //            else
    //            {
    //                errorsOccurred = true;
    //                errors.AddRange(validationResult.Errors.Select(e => $"{(!string.IsNullOrWhiteSpace(item.Name) ? $"{item.Name} - " : string.Empty)}{e.ErrorMessage}"));
    //            }
    //        }

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

    public async Task<byte[]> Handle(CreateMappingRulesTemplateCommand request, CancellationToken cancellationToken)
    {

        var fields = new string[] {
                   _localizer["Mapping Rule Name"],
                   _localizer["Active"],
                   _localizer["Import Parameter Field Name 1"],
                   _localizer["Legacy Field Name 1"],
                   _localizer["Legacy Field Description 1"],
                   _localizer["Import Parameter Field Name 2"],
                   _localizer["Legacy Field Name 2"],
                   _localizer["Legacy Field Description 2"],
                   _localizer["Import Parameter Field Name 3"],
                   _localizer["Legacy Field Name 3"],
                   _localizer["Legacy Field Description 3"],
                   _localizer["Export Parameter Field"],
                   _localizer["New Field Name"],
                   _localizer["New Field Description"],
                   _localizer["Is Mock"],
                   _localizer["Team"],
                   _localizer["Legacy System"],
                   _localizer["Project Name"],
                   _localizer["Relevant Objects"],
                   _localizer["Comments"],
            };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["MappingRules"]);
        return result;
    }
}

