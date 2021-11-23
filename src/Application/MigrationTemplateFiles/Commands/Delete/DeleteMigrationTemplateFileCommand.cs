// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Delete;

    public class DeleteMigrationTemplateFileCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedMigrationTemplateFilesCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteMigrationTemplateFileCommandHandler : 
                 IRequestHandler<DeleteMigrationTemplateFileCommand, Result>,
                 IRequestHandler<DeleteCheckedMigrationTemplateFilesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteMigrationTemplateFileCommandHandler> _localizer;
        public DeleteMigrationTemplateFileCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteMigrationTemplateFileCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteMigrationTemplateFileCommand request, CancellationToken cancellationToken)
        {
         
           var item = await _context.MigrationTemplateFiles.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.MigrationTemplateFiles.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedMigrationTemplateFilesCommand request, CancellationToken cancellationToken)
        {
          
           var items = await _context.MigrationTemplateFiles.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.MigrationTemplateFiles.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

