// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.Import;

    public class ImportMigrationProjectsCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateMigrationProjectsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportMigrationProjectsCommandHandler : 
                 IRequestHandler<CreateMigrationProjectsTemplateCommand, byte[]>,
                 IRequestHandler<ImportMigrationProjectsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportMigrationProjectsCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportMigrationProjectsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportMigrationProjectsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportMigrationProjectsCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MigrationProjectDto, object>>
            {
               { _localizer["Project Name"], (row,item) => item.Name = row[_localizer["Project Name"]]?.ToString() },
               { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]]?.ToString() },
               { _localizer["Status"], (row,item) => item.Status = row[_localizer["Status"]]?.ToString() },
               { _localizer["Begin Date"], (row,item) => item.BeginDateTime = row.IsNull(_localizer["Begin Date"])?DateTime.Now:Convert.ToDateTime(row[_localizer["Begin Date"]]?.ToString()) },

            }, _localizer["MigrationProjects"]);
           throw new System.NotImplementedException();
        }
        public async Task<byte[]> Handle(CreateMigrationProjectsTemplateCommand request, CancellationToken cancellationToken)
        {
  
            var fields = new string[] {
                 
                   _localizer["Project Name"],
                   _localizer["Status"],
                   _localizer["Begin Date"],
                   _localizer["Description"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["MigrationProjects"]);
            return result;
        }
    }

