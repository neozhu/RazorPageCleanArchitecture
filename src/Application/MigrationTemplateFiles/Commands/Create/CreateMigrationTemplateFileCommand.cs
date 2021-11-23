// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Create;

    public class CreateMigrationTemplateFileCommand: MigrationTemplateFileDto,IRequest<Result<int>>, IMapFrom<MigrationTemplateFile>
    {
       
    }
    
    public class CreateMigrationTemplateFileCommandHandler : IRequestHandler<CreateMigrationTemplateFileCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateMigrationTemplateFileCommand> _localizer;
        public CreateMigrationTemplateFileCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateMigrationTemplateFileCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateMigrationTemplateFileCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<MigrationTemplateFile>(request);
           _context.MigrationTemplateFiles.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

