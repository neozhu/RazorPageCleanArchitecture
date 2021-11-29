// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.AddEdit;

    public class AddEditMigrationProjectCommand: MigrationProjectDto,IRequest<Result<int>>, IMapFrom<MigrationProject>
    {
      
    }

    public class AddEditMigrationProjectCommandHandler : IRequestHandler<AddEditMigrationProjectCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMigrationProjectCommandHandler> _localizer;
        public AddEditMigrationProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditMigrationProjectCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditMigrationProjectCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing AddEditMigrationProjectCommandHandler method 
            if (request.Id > 0)
            {
                var item = await _context.MigrationProjects.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<MigrationProject>(request);
                _context.MigrationProjects.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

