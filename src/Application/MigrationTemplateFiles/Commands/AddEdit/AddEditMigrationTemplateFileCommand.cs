// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.AddEdit;

    public class AddEditMigrationTemplateFileCommand: MigrationTemplateFileDto,IRequest<Result<int>>, IMapFrom<MigrationTemplateFile>
    {
      
    }

    public class AddEditMigrationTemplateFileCommandHandler : IRequestHandler<AddEditMigrationTemplateFileCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMigrationTemplateFileCommandHandler> _localizer;
        public AddEditMigrationTemplateFileCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditMigrationTemplateFileCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditMigrationTemplateFileCommand request, CancellationToken cancellationToken)
        {
            
            if (request.Id > 0)
            {
                var item = await _context.MigrationTemplateFiles.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<MigrationTemplateFile>(request);
                _context.MigrationTemplateFiles.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

