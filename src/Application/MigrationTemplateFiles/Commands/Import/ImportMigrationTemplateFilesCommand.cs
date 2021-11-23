// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Import;

    public class ImportMigrationTemplateFilesCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateMigrationTemplateFilesTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportMigrationTemplateFilesCommandHandler : 
                 IRequestHandler<CreateMigrationTemplateFilesTemplateCommand, byte[]>,
                 IRequestHandler<ImportMigrationTemplateFilesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportMigrationTemplateFilesCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportMigrationTemplateFilesCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportMigrationTemplateFilesCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportMigrationTemplateFilesCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing ImportMigrationTemplateFilesCommandHandler method
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MigrationTemplateFileDto, object>>
            {
                //ex. { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },

            }, _localizer["MigrationTemplateFiles"]);
           throw new System.NotImplementedException();
        }
        public async Task<byte[]> Handle(CreateMigrationTemplateFilesTemplateCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ImportMigrationTemplateFilesCommandHandler method 
            var fields = new string[] {
                   //TODO:Defines the title and order of the fields to be imported's template
                   //_localizer["Name"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["MigrationTemplateFiles"]);
            return result;
        }
    }

