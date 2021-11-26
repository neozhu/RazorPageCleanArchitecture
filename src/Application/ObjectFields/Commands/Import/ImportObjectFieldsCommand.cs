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
                { _localizer["Field Name"], (row,item) => item.Name = row[_localizer["Field Name"]]?.ToString() },
                { _localizer["Field Description"], (row,item) => item.Description = row[_localizer["Field Description"]]?.ToString() },
                { _localizer["Associated Type"], (row,item) => item.AssociatedType = row[_localizer["Associated Type"]]?.ToString() },
                { _localizer["Date Type"], (row,item) => item.DateType = row[_localizer["DateType"]]?.ToString() },
                { _localizer["Length"], (row,item) => item.Length =row.IsNull(_localizer["Length"])?null: Convert.ToInt32(row[_localizer["Length"]]?.ToString()) },
                { _localizer["Direct"], (row,item) => item.Direct = row[_localizer["Direct"]]?.ToString() },
                { _localizer["Parameter Name"], (row,item) => item.ParameterName = row[_localizer["Parameter Name"]]?.ToString() },
                { _localizer["Title"], (row,item) => item.Title = row[_localizer["Title"]]?.ToString() },
                { _localizer["Source Template Name"], (row,item) => item.SourceTemplateName = row[_localizer["SourceTemplateName"]]?.ToString() },
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
            _localizer["Field Name"],
            _localizer["Field Description"],
            _localizer["Associated Type"],
            _localizer["Date Type"],
            _localizer["Length"],
            _localizer["Parameter Name"],
            _localizer["Direct"],
            _localizer["Title"],
            _localizer["Source Template Name"],
          
        };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["ObjectFields"]);
        return result;
    }
}

