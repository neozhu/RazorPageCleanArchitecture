// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.Delete;

public class DeleteMigrationObjectCommand : IRequest<Result>
{
    public int Id { get; set; }
}
public class DeleteCheckedMigrationObjectsCommand : IRequest<Result>
{
    public int[] Id { get; set; }
}

public class DeleteMigrationObjectCommandHandler :
             IRequestHandler<DeleteMigrationObjectCommand, Result>,
             IRequestHandler<DeleteCheckedMigrationObjectsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<DeleteMigrationObjectCommandHandler> _localizer;
    public DeleteMigrationObjectCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<DeleteMigrationObjectCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result> Handle(DeleteMigrationObjectCommand request, CancellationToken cancellationToken)
    {

        var item = await _context.MigrationObjects.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item == null)
        {
            throw new NotFoundException($"Not found Migration Objects by Id:{request.Id}");
        }
        _context.MigrationObjects.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> Handle(DeleteCheckedMigrationObjectsCommand request, CancellationToken cancellationToken)
    {

        var items = await _context.MigrationObjects.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
            _context.MigrationObjects.Remove(item);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

