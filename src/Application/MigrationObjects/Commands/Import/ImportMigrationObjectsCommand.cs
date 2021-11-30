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
   
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MigrationObjectDto, object>>
            {
                { _localizer["Project Name"], (row,item) => item.ProjectName = row[_localizer["Project Name"]]?.ToString() },
                { _localizer["Conversion Object Name"], (row,item) => item.Name = row[_localizer["Conversion Object Name"]]?.ToString() },
                { _localizer["Object Name"], (row,item) => item.ObjectName = row[_localizer["Object Name"]]?.ToString() },
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
                var migrationProject =await _context.MigrationProjects.FirstOrDefaultAsync(x => x.Name == item.ProjectName);
                if (migrationProject == null)
                {
                    errorsOccurred = true;
                    errors.Add($"not found migration project by name:{item.ProjectName}");
                    break;
                }
                var createdto = new CreateMigrationObjectCommand() {
                     ProjectName = item.ProjectName,
                     Name = item.Name,
                     Team=item.Team,
                     Description=item.Description
                    };
                var validationResult = await _addValidator.ValidateAsync(createdto, cancellationToken);
                if (validationResult.IsValid)
                {
                    var exist = await _context.MigrationObjects.AnyAsync(x => x.Name == item.Name , cancellationToken);
                    if (!exist)
                    {
                        var newitem = _mapper.Map<MigrationObject>(item);
                        newitem.MigrationProjectId = migrationProject.Id;
                        newitem.MigrationProject = migrationProject;
                        await _context.MigrationObjects.AddAsync(newitem, cancellationToken);
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
                   _localizer["Project Name"],
                   _localizer["Conversion Object Name"],
                   _localizer["Object Name"],
                   _localizer["Description"],
                   _localizer["Team"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["MigrationObjects"]);
            return result;
        }
    }

