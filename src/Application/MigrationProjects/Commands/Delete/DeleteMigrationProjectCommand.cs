// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.Delete;

    public class DeleteMigrationProjectCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedMigrationProjectsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteMigrationProjectCommandHandler : 
                 IRequestHandler<DeleteMigrationProjectCommand, Result>,
                 IRequestHandler<DeleteCheckedMigrationProjectsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteMigrationProjectCommandHandler> _localizer;
        public DeleteMigrationProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteMigrationProjectCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteMigrationProjectCommand request, CancellationToken cancellationToken)
        {

           var item = await _context.MigrationProjects.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.MigrationProjects.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedMigrationProjectsCommand request, CancellationToken cancellationToken)
        {

           var items = await _context.MigrationProjects.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.MigrationProjects.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

