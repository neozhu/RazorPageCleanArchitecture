// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Update;

    public class UpdateMigrationTemplateFileCommand: MigrationTemplateFileDto,IRequest<Result>, IMapFrom<MigrationTemplateFile>
    {
        
    }

    public class UpdateMigrationTemplateFileCommandHandler : IRequestHandler<UpdateMigrationTemplateFileCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateMigrationTemplateFileCommandHandler> _localizer;
        public UpdateMigrationTemplateFileCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateMigrationTemplateFileCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateMigrationTemplateFileCommand request, CancellationToken cancellationToken)
        {
          
           var item =await _context.MigrationTemplateFiles.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

