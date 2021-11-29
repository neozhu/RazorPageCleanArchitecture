// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationProjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationProjects.Commands.AcceptChanges;

    public class AcceptChangesMigrationProjectsCommand:IRequest<Result>
    {
      public MigrationProjectDto[] Items { get; set; }
    }

    public class AcceptChangesMigrationProjectsCommandHandler : IRequestHandler<AcceptChangesMigrationProjectsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AcceptChangesMigrationProjectsCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AcceptChangesMigrationProjectsCommand request, CancellationToken cancellationToken)
        {
            foreach(var item in request.Items)
            {
                switch (item.TrackingState)
                {
                    case TrackingState.Added:
                        var newitem = _mapper.Map<MigrationProject>(item);
                        await _context.MigrationProjects.AddAsync(newitem, cancellationToken);
                        break;
                    case TrackingState.Deleted:
                        var delitem =await _context.MigrationProjects.FindAsync(new object[] { item.Id }, cancellationToken);
                        _context.MigrationProjects.Remove(delitem);
                        break;
                    case TrackingState.Modified:
                        var edititem = await _context.MigrationProjects.FindAsync(new object[] { item.Id }, cancellationToken);
                        edititem = _mapper.Map(item, edititem);
                        _context.MigrationProjects.Update(edititem);
                        break;
                    case TrackingState.Unchanged:
                    default:
                        break;
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }

