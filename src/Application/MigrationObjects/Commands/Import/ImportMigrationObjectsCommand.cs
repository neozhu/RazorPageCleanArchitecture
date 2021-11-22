// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationObjects.Commands.Create;
using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.Import;

    public class ImportMigrationObjectsCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateMigrationObjectsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportMigrationObjectsCommandHandler : 
                 IRequestHandler<CreateMigrationObjectsTemplateCommand, byte[]>,
                 IRequestHandler<ImportMigrationObjectsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportMigrationObjectsCommandHandler> _localizer;
    private readonly IValidator<CreateMigrationObjectCommand> _addValidator;
    private readonly IExcelService _excelService;

        public ImportMigrationObjectsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportMigrationObjectsCommandHandler> localizer,
            IValidator<CreateMigrationObjectCommand> addValidator,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
        _addValidator = addValidator;
        _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportMigrationObjectsCommand request, CancellationToken cancellationToken)
        {
   
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MigrationObject, object>>
            {
                { _localizer["Object Name"], (row,item) => item.Name = row[_localizer["Object Name"]]?.ToString() },
                { _localizer["Team"], (row,item) => item.Team = row[_localizer["Team"]]?.ToString() },
                { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]]?.ToString() },

            }, _localizer["MigrationObjects"]);
        if (result.Succeeded)
        {
            var importItems = result.Data;
            var errors = new List<string>();
            var errorsOccurred = false;
            foreach (var item in importItems)
            {
                var validationResult = await _addValidator.ValidateAsync(_mapper.Map<CreateMigrationObjectCommand>(item), cancellationToken);
                if (validationResult.IsValid)
                {
                    var exist = await _context.MigrationObjects.AnyAsync(x => x.Name == item.Name , cancellationToken);
                    if (!exist)
                    {
                 
                        await _context.MigrationObjects.AddAsync(item, cancellationToken);
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
        public async Task<byte[]> Handle(CreateMigrationObjectsTemplateCommand request, CancellationToken cancellationToken)
        {

            var fields = new string[] {
                  
                   _localizer["Object Name"],
                   _localizer["Team"],
                   _localizer["Description"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["MigrationObjects"]);
            return result;
        }
    }

