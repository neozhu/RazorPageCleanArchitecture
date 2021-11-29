// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.Update;

    public class UpdateMigrationProjectCommand: MigrationProjectDto,IRequest<Result>, IMapFrom<MigrationProject>
    {
        
    }

    public class UpdateMigrationProjectCommandHandler : IRequestHandler<UpdateMigrationProjectCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateMigrationProjectCommandHandler> _localizer;
        public UpdateMigrationProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateMigrationProjectCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateMigrationProjectCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing UpdateMigrationProjectCommandHandler method 
           var item =await _context.MigrationProjects.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

