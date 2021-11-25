// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.AcceptChanges;

public class AcceptChangesFieldMappingValuesCommand : IRequest<Result>
{
    public FieldMappingValueDto[] Items { get; set; }
}

public class AcceptChangesFieldMappingValuesCommandHandler : IRequestHandler<AcceptChangesFieldMappingValuesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AcceptChangesFieldMappingValuesCommandHandler(
        IApplicationDbContext context,
         IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result> Handle(AcceptChangesFieldMappingValuesCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            switch (item.TrackingState)
            {
                case TrackingState.Added:
                    var newitem = _mapper.Map<FieldMappingValue>(item);
                    await _context.FieldMappingValues.AddAsync(newitem, cancellationToken);
                    break;
                case TrackingState.Deleted:
                    var delitem = await _context.FieldMappingValues.FindAsync(new object[] { item.Id }, cancellationToken);
                    _context.FieldMappingValues.Remove(delitem);
                    break;
                case TrackingState.Modified:
                    var edititem = await _context.FieldMappingValues.FindAsync(new object[] { item.Id }, cancellationToken);
                    edititem = _mapper.Map(item, edititem);
                    _context.FieldMappingValues.Update(edititem);
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

