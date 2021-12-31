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
        var array_id = request.Items.Select(x => x.MappingRuleId).Distinct().ToArray();
        foreach (var item in request.Items.DistinctBy(x => new {x.Legacy1,x.Legacy2,x.Legacy3,x.NewValue }))
        {
            switch (item.TrackingState)
            {
                case TrackingState.Added:
                    var newitem = _mapper.Map<FieldMappingValue>(item);
                    var isexists = await _context.FieldMappingValues.AnyAsync(x => x.MappingRuleId == newitem.MappingRuleId &&
                                                                              x.Legacy1 == newitem.Legacy1 &&
                                                                              x.Legacy2 == newitem.Legacy2 &&
                                                                              x.Legacy3 == newitem.Legacy3 &&
                                                                              x.NewValue == newitem.NewValue);
                    if (!isexists)
                    {
                        await _context.FieldMappingValues.AddAsync(newitem, cancellationToken);
                    }
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

        var mappingrules = await _context.MappingRules.Where(x => array_id.Contains(x.Id)).ToListAsync();
        foreach(var item in mappingrules)
        {
            var hasdata = await _context.FieldMappingValues.AnyAsync(x => x.MappingRuleId == item.Id);
            if (hasdata)
            {
                item.Active = "Active";
            }
            else
            {
                item.Active = "Inactive";
            }
            item.Status = "Ongoing";
            _context.MappingRules.Update(item);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}

