using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.ChangeStatus;

public record FinishedMappingRuleStatusCommand: IRequest<Result>
{
    public int[] Id { get; set; }
}

public class FinishedMappingRuleStatusCommandHandler:
    IRequestHandler<FinishedMappingRuleStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<FinishedMappingRuleStatusCommandHandler> _logger;

    public FinishedMappingRuleStatusCommandHandler(
        IApplicationDbContext context,
        ILogger<FinishedMappingRuleStatusCommandHandler> logger
        )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(FinishedMappingRuleStatusCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.MappingRules.Where(x=>request.Id.Contains(x.Id)).ToListAsync();
        foreach (var item in items)
        {
            item.Status = "Finished";
            _context.MappingRules.Update(item);
        }
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Set the value mapping rule status to finished:{@Request}", request);
        return Result.Success();

    }
}