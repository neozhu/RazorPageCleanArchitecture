using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.ChangeStatus;

public record FinishedMappingRuleStatusCommand: IRequest<Result>
{
    public int Id { get; set; }
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
        var item = await _context.MappingRules.FindAsync(request.Id);
        if (item == null)
        {
            return Result.Failure(new string[] { $"Not found value mapping rule." });
        }
        item.Status = "Finished";
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Set the status of value mapping rule to finished:{@Request}",request);
        return Result.Success();

    }
}