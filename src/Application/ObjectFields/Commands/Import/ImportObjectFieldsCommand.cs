// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.Commands.Create;
using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Import;

public class ImportObjectFieldsCommand : IRequest<Result>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}
public class CreateObjectFieldsTemplateCommand : IRequest<byte[]>
{
    public IEnumerable<string> Fields { get; set; }
    public string SheetName { get; set; }
}

public class ImportObjectFieldsCommandHandler :
             IRequestHandler<CreateObjectFieldsTemplateCommand, byte[]>,
             IRequestHandler<ImportObjectFieldsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ImportObjectFieldsCommandHandler> _localizer;
    private readonly IValidator<CreateObjectFieldCommand> _addValidator;
    private readonly IExcelService _excelService;

    public ImportObjectFieldsCommandHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IStringLocalizer<ImportObjectFieldsCommandHandler> localizer,
        IValidator<CreateObjectFieldCommand> addValidator,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _addValidator = addValidator;
        _excelService = excelService;
        _mapper = mapper;
    }
    public async Task<Result> Handle(ImportObjectFieldsCommand request, CancellationToken cancellationToken)
    {

        var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, ObjectField, object>>
            {
                { _localizer["Field"], (row,item) => item.Name = row[_localizer["Field"]]?.ToString() },
                { _localizer["Field Description"], (row,item) => item.Description = row[_localizer["Field Description"]]?.ToString() },
                { _localizer["Master Data Relevant"], (row,item) => item.MasterDataRelevant = row[_localizer["Master Data Relevant"]]?.ToString() },
                { _localizer["Tech Mock Master Data"], (row,item) => item.TechMockMasterData = row[_localizer["Tech Mock Master Data"]]?.ToString() },
                { _localizer["Team"], (row,item) => item.Team = row[_localizer["Team"]]?.ToString() },
                { _localizer["Status"], (row,item) => item.Status = row[_localizer["Status"]]?.ToString() },
                { _localizer["Link"], (row,item) => item.Link = row[_localizer["Link"]]?.ToString() },
                { _localizer["Legacy System"], (row,item) => item.LegacySystem = row[_localizer["Legacy System"]]?.ToString() },
                { _localizer["Is Used AK1"], (row,item) => item.IsUsedAK1 = row[_localizer["Is Used AK1"]]?.ToString() },
                { _localizer["Major Table where object is used"], (row,item) => item.MajorTable = row[_localizer["Major Table where object is used"]]?.ToString() },
                { _localizer["Nr. of Cases where used"], (row,item) => item.Cases = row[_localizer["Nr. of Cases where used"]]?.ToString() },
                { _localizer["Numbers if different values"], (row,item) => item.Numbers = row[_localizer["Numbers if different values"]]?.ToString() },
                { _localizer["Relevant Migration Objects"], (row,item) => item.RelevantObjects = row[_localizer["Relevant Migration Objects"]]?.ToString() },
                { _localizer["Check"], (row,item) => item.Check = row[_localizer["Check"]]?.ToString() },
                { _localizer["Comments"], (row,item) => item.Comments = row[_localizer["Comments"]]?.ToString() },
            }, _localizer["ObjectFields"]);
        if (result.Succeeded)
        {
            var importItems = result.Data;
            var errors = new List<string>();
            var errorsOccurred = false;
            foreach (var item in importItems)
            {
                var validationResult = await _addValidator.ValidateAsync(_mapper.Map<CreateObjectFieldCommand>(item), cancellationToken);
                if (validationResult.IsValid)
                {
                    var exist = await _context.ObjectFields.AnyAsync(x => x.Name == item.Name, cancellationToken);
                    if (!exist)
                    {

                        await _context.ObjectFields.AddAsync(item, cancellationToken);
                    }
                }
                else
                {
                    errorsOccurred = true;
                    errors.AddRange(validationResult.Errors.Select(e => $"{(!string.IsNullOrWhiteSpace(item.Name) ? $"{item.Name} - " : string.Empty)}{e.ErrorMessage}"));
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
    public async Task<byte[]> Handle(CreateObjectFieldsTemplateCommand request, CancellationToken cancellationToken)
    {
      
        var fields = new string[] {
            _localizer["Field"],
            _localizer["Field Description"],
            _localizer["Master Data Relevant"],
            _localizer["Tech Mock Master Data"],
            _localizer["Team"],
            _localizer["Status"],
            _localizer["Link"],
            _localizer["Legacy System"],
            _localizer["Is Used AK1"],
            _localizer["Major Table where object is used"],
            _localizer["Nr. of Cases where used"],
            _localizer["Numbers if different values"],
            _localizer["Relevant Migration Objects"],
            _localizer["Check"],
            _localizer["Comments"]
        };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["ObjectFields"]);
        return result;
    }
}

