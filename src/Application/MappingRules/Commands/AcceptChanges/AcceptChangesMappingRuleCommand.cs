// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.AcceptChanges;

public class AcceptChangesMappingRulesCommand : IRequest<Result>
{
    public MappingRuleDto[] Items { get; set; }
}

public class AcceptChangesMappingRulesCommandHandler : IRequestHandler<AcceptChangesMappingRulesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AcceptChangesMappingRulesCommandHandler(
        IApplicationDbContext context,
         IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result> Handle(AcceptChangesMappingRulesCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            switch (item.TrackingState)
            {
                case TrackingState.Added:
                    var newitem = _mapper.Map<MappingRule>(item);
                    await _context.MappingRules.AddAsync(newitem, cancellationToken);
                    break;
                case TrackingState.Deleted:
                    var delitem = await _context.MappingRules.FindAsync(new object[] { item.Id }, cancellationToken);
                    _context.MappingRules.Remove(delitem);
                    break;
                case TrackingState.Modified:
                    var edititem = await _context.MappingRules.FindAsync(new object[] { item.Id }, cancellationToken);
                    edititem = _mapper.Map(item, edititem);
                    _context.MappingRules.Update(edititem);
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

