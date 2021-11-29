// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.Create;

    public class CreateMigrationProjectCommand: MigrationProjectDto,IRequest<Result<int>>, IMapFrom<MigrationProject>
    {
       
    }
    
    public class CreateMigrationProjectCommandHandler : IRequestHandler<CreateMigrationProjectCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateMigrationProjectCommand> _localizer;
        public CreateMigrationProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateMigrationProjectCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateMigrationProjectCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing CreateMigrationProjectCommandHandler method 
           var item = _mapper.Map<MigrationProject>(request);
           _context.MigrationProjects.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

