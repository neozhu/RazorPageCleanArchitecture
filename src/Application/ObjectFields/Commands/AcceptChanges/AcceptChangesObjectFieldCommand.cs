// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.AcceptChanges;

public class AcceptChangesObjectFieldsCommand : IRequest<Result>
{
    public ObjectFieldDto[] Items { get; set; }
}

public class AcceptChangesObjectFieldsCommandHandler : IRequestHandler<AcceptChangesObjectFieldsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AcceptChangesObjectFieldsCommandHandler(
        IApplicationDbContext context,
         IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result> Handle(AcceptChangesObjectFieldsCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            switch (item.TrackingState)
            {
                case TrackingState.Added:
                    var newitem = _mapper.Map<ObjectField>(item);
                    await _context.ObjectFields.AddAsync(newitem, cancellationToken);
                    break;
                case TrackingState.Deleted:
                    var delitem = await _context.ObjectFields.FindAsync(new object[] { item.Id }, cancellationToken);
                    _context.ObjectFields.Remove(delitem);
                    break;
                case TrackingState.Modified:
                    var edititem = await _context.ObjectFields.FindAsync(new object[] { item.Id }, cancellationToken);
                    edititem = _mapper.Map(item, edititem);
                    _context.ObjectFields.Update(edititem);
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

