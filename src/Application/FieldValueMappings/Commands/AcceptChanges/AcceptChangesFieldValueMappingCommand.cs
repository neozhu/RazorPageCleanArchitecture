// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.AcceptChanges;

public class AcceptChangesFieldValueMappingsCommand : IRequest<Result>
{
    public FieldValueMappingDto[] Items { get; set; }
}

public class AcceptChangesFieldValueMappingsCommandHandler : IRequestHandler<AcceptChangesFieldValueMappingsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AcceptChangesFieldValueMappingsCommandHandler(
        IApplicationDbContext context,
         IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result> Handle(AcceptChangesFieldValueMappingsCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            switch (item.TrackingState)
            {
                case TrackingState.Added:
                    var newitem = _mapper.Map<FieldValueMapping>(item);
                    await _context.FieldValueMappings.AddAsync(newitem, cancellationToken);
                    break;
                case TrackingState.Deleted:
                    var delitem = await _context.FieldValueMappings.FindAsync(new object[] { item.Id }, cancellationToken);
                    _context.FieldValueMappings.Remove(delitem);
                    break;
                case TrackingState.Modified:
                    var edititem = await _context.FieldValueMappings.FindAsync(new object[] { item.Id }, cancellationToken);
                    edititem = _mapper.Map(item, edititem);
                    _context.FieldValueMappings.Update(edititem);
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

