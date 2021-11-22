// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.AcceptChanges;

public class AcceptChangesMigrationObjectsCommand : IRequest<Result>
{
    public MigrationObjectDto[] Items { get; set; }
}

public class AcceptChangesMigrationObjectsCommandHandler : IRequestHandler<AcceptChangesMigrationObjectsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AcceptChangesMigrationObjectsCommandHandler(
        IApplicationDbContext context,
         IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result> Handle(AcceptChangesMigrationObjectsCommand request, CancellationToken cancellationToken)
    {

        foreach (var item in request.Items)
        {
            switch (item.TrackingState)
            {
                case TrackingState.Added:
                    var newitem = _mapper.Map<MigrationObject>(item);
                    await _context.MigrationObjects.AddAsync(newitem, cancellationToken);
                    break;
                case TrackingState.Deleted:
                    var delitem = await _context.MigrationObjects.FindAsync(new object[] { item.Id }, cancellationToken);
                    _context.MigrationObjects.Remove(delitem);
                    break;
                case TrackingState.Modified:
                    var edititem = await _context.MigrationObjects.FindAsync(new object[] { item.Id }, cancellationToken);
                    edititem.Name = item.Name;
                    edititem.Description = item.Description;
                    edititem.Team = item.Team;
                    _context.MigrationObjects.Update(edititem);
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

