// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldValueMappings.Commands.Create;
using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.Import;

public class ImportFieldValueMappingsCommand : IRequest<Result>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}
public class CreateFieldValueMappingsTemplateCommand : IRequest<byte[]>
{
    public IEnumerable<string> Fields { get; set; }
    public string SheetName { get; set; }
}

public class ImportFieldValueMappingsCommandHandler :
             IRequestHandler<CreateFieldValueMappingsTemplateCommand, byte[]>,
             IRequestHandler<ImportFieldValueMappingsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ImportFieldValueMappingsCommandHandler> _localizer;
    private readonly IValidator<CreateFieldValueMappingCommand> _addValidator;
    private readonly IExcelService _excelService;

    public ImportFieldValueMappingsCommandHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IStringLocalizer<ImportFieldValueMappingsCommandHandler> localizer,
        IValidator<CreateFieldValueMappingCommand> addValidator,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _addValidator = addValidator;
        _excelService = excelService;
        _mapper = mapper;
    }
    public async Task<Result> Handle(ImportFieldValueMappingsCommand request, CancellationToken cancellationToken)
    {

        var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, FieldValueMappingDto, object>>
            {
                { _localizer["Field"], (row,item) => item.FieldName = row[_localizer["Field"]]?.ToString() },
                { _localizer["Field Description"], (row,item) => item.Description = row[_localizer["Field Description"]]?.ToString() },
                { _localizer["Stage"], (row,item) => item.Stage = row.IsNull(_localizer["Stage"])?"Mock1":row[_localizer["Stage"]].ToString() },
                { _localizer["Legacy 1"], (row,item) => item.Legacy1 = row[_localizer["Legacy 1"]]?.ToString() },
                { _localizer["Legacy 2"], (row,item) => item.Legacy2 = row[_localizer["Legacy 2"]]?.ToString() },
                { _localizer["Legacy 3"], (row,item) => item.Legacy3 = row[_localizer["Legacy 3"]]?.ToString() },
                { _localizer["New"], (row,item) => item.NewValue = row[_localizer["New"]]?.ToString() },
                { _localizer["Text Legacy System"], (row,item) => item.LegacySystem = row[_localizer["Text Legacy System"]]?.ToString() },
                { _localizer["Comments"], (row,item) => item.Comments = row[_localizer["Comments"]]?.ToString() },
            }, _localizer["FieldValueMappings"]);

        if (result.Succeeded)
        {
            var importItems = result.Data;
            var errors = new List<string>();
            var errorsOccurred = false;
            var objectlist = new List<ObjectField>();
            foreach (var item in importItems)
            {
                var createcommand = new CreateFieldValueMappingCommand()
                {
                    Check = item.Check,
                    LegacySystem = item.LegacySystem,
                    Description = item.Description,
                    FieldName = item.FieldName,
                    Comments = item.Comments,
                    Legacy1 = item.Legacy1,
                    Legacy2 = item.Legacy2,
                    Legacy3 = item.Legacy3,
                    Legacy4 = item.Legacy4,
                    NewValue = item.NewValue,
                    ObjectFieldId = item.ObjectFieldId,
                    Stage = item.Stage,
                    Team = item.Team

                };
                var validationResult = await _addValidator.ValidateAsync(createcommand, cancellationToken);
                if (validationResult.IsValid)
                {
                    var objectfield = await _context.ObjectFields.Where(x => x.Name == item.FieldName).FirstOrDefaultAsync();
                    if (objectfield == null)
                    {
                        objectfield = objectlist.FirstOrDefault(x=>x.Name == item.FieldName);
                        if (objectfield == null)
                        {
                            objectfield = new ObjectField()
                            {
                                Name = item.FieldName,
                                Description = item.Description,
                                LegacySystem = item.LegacySystem,
                                Team = item.Team,
                            };
                            await _context.ObjectFields.AddAsync(objectfield);
                            objectlist.Add(objectfield);
                        }
                    }
                    var valuemapping = await _context.FieldValueMappings.FirstOrDefaultAsync(x => x.ObjectFieldId == objectfield.Id && x.Stage == item.Stage && x.Legacy1 == item.Legacy1 && x.NewValue == x.NewValue, cancellationToken);
                    if (valuemapping == null)
                    {
                        var createitem = _mapper.Map<FieldValueMapping>(item);
                        createitem.Description = objectfield.Description;
                        createitem.LegacySystem = objectfield.LegacySystem;
                        createitem.ObjectFieldId = objectfield.Id;
                        createitem.ObjectField= objectfield;
                        await _context.FieldValueMappings.AddAsync(createitem, cancellationToken);
                    }
                    else
                    {

                    }
                }
                else
                {
                    errorsOccurred = true;
                    errors.AddRange(validationResult.Errors.Select(e => $"{(!string.IsNullOrWhiteSpace(item.FieldName) ? $"{item.FieldName} - " : string.Empty)}{e.ErrorMessage}"));
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
    public async Task<byte[]> Handle(CreateFieldValueMappingsTemplateCommand request, CancellationToken cancellationToken)
    {

        var fields = new string[] {
                   _localizer["Field"],
                   _localizer["Field Description"],
                   _localizer["Stage"],
                   _localizer["Legacy 1"],
                   _localizer["Legacy 2"],
                   _localizer["Legacy 3"],
                   _localizer["New"],
                   _localizer["Text Legacy System"],
                   _localizer["Comments"],
                };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["FieldValueMappings"]);
        return result;
    }
}

