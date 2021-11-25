// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Create;
using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Import;

public class ImportFieldMappingValuesCommand : IRequest<Result>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}
public class CreateFieldMappingValuesTemplateCommand : IRequest<byte[]>
{
    public IEnumerable<string> Fields { get; set; }
    public string SheetName { get; set; }
}

public class ImportFieldMappingValuesCommandHandler :
             IRequestHandler<CreateFieldMappingValuesTemplateCommand, byte[]>,
             IRequestHandler<ImportFieldMappingValuesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ImportFieldMappingValuesCommandHandler> _localizer;
    private readonly IValidator<CreateFieldMappingValueCommand> _validator;
    private readonly IExcelService _excelService;

    public ImportFieldMappingValuesCommandHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IStringLocalizer<ImportFieldMappingValuesCommandHandler> localizer,
        IValidator<CreateFieldMappingValueCommand> validator,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _validator = validator;
        _excelService = excelService;
        _mapper = mapper;
    }
    public async Task<Result> Handle(ImportFieldMappingValuesCommand request, CancellationToken cancellationToken)
    {
        var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, FieldMappingValueDto, object>>
            {
                { _localizer["Mapping Rule Name"], (row,item) => item.MappingRule = row[_localizer["Mapping Rule Name"]]?.ToString() },
                { _localizer["Mock"], (row,item) => item.Mock = row[_localizer["Mock"]]?.ToString() },
                { _localizer["Legacy 1"], (row,item) => item.Legacy1 = row[_localizer["Legacy 1"]]?.ToString() },
                { _localizer["Legacy 2"], (row,item) => item.Legacy2 = row[_localizer["Legacy 2"]]?.ToString() },
                { _localizer["Legacy 3"], (row,item) => item.Legacy3 = row[_localizer["Legacy 3"]]?.ToString() },
                { _localizer["New Value"], (row,item) => item.NewValue = row[_localizer["New Value"]]?.ToString() },
                { _localizer["Comments"], (row,item) => item.Comments = row[_localizer["Comments"]]?.ToString() },

            }, _localizer["FieldMappingValues"]);
        if (result.Succeeded)
        {
            var importItems = result.Data;
            var errors = new List<string>();
            var errorsOccurred = false;
            var rulelist = new List<MappingRule>();
            foreach (var item in importItems)
            {
                var validateitem = new CreateFieldMappingValueCommand()
                {
                    Check = item.Check,
                    Comments = item.Comments,
                    MappingRuleId = item.MappingRuleId,
                    MappingRule = item.MappingRule,
                    Mock = item.Mock,
                    Legacy1 = item.Legacy1,
                    Legacy2 = item.Legacy2,
                    Legacy3 = item.Legacy3,
                    NewValue = item.NewValue,
                    LegacySystem = item.LegacySystem,
                    Description = item.Description,
                    Team = item.Team,
                };
                var validationResult = await _validator.ValidateAsync(validateitem, cancellationToken);
                if (validationResult.IsValid)
                {
                    var mappingrule = await _context.MappingRules.FirstOrDefaultAsync(x => x.Name == item.MappingRule);
                    if (mappingrule == null)
                    {
                        mappingrule = rulelist.FirstOrDefault(x => x.Name == item.MappingRule);
                        if (mappingrule == null)
                        {
                            mappingrule = new MappingRule()
                            {
                                Name = item.MappingRule,
                                IsMock = string.IsNullOrEmpty(item.Mock) ? false : true,
                            };

                            rulelist.Add(mappingrule);
                            await _context.MappingRules.AddAsync(mappingrule, cancellationToken);
                        }
                    }
                    var exist = await _context.FieldMappingValues.FirstOrDefaultAsync(x =>
                            x.MappingRuleId == mappingrule.Id
                         && x.Legacy1 == item.Legacy1
                         && x.Legacy2 == item.Legacy2
                         && x.Legacy3 == item.Legacy3
                         && x.NewValue == item.NewValue
                         , cancellationToken);
                    if (exist == null)
                    {
                        var newitem = _mapper.Map<FieldMappingValue>(item);
                        newitem.MappingRuleId = mappingrule.Id;
                        newitem.LegacySystem = mappingrule.LegacySystem;
                        await _context.FieldMappingValues.AddAsync(newitem, cancellationToken);
                    }

                }
                else
                {
                    errorsOccurred = true;
                    errors.AddRange(validationResult.Errors.Select(e => $"{(!string.IsNullOrWhiteSpace(item.MappingRule) ? $"{item.MappingRule} - " : "Mapping Rule Name")}{e.ErrorMessage}"));
                }
            }

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
    public async Task<byte[]> Handle(CreateFieldMappingValuesTemplateCommand request, CancellationToken cancellationToken)
    {

        var fields = new string[] {
                   _localizer["Mapping Rule Name"],
                   _localizer["Mock"],
                   _localizer["Legacy 1"],
                   _localizer["Legacy 2"],
                   _localizer["Legacy 3"],
                   _localizer["New Value"],
                   _localizer["Comments"],
                };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["FieldMappingValues"]);
        return result;
    }
}

