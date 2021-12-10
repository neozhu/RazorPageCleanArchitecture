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

    public FinishedMappingRuleStatusCommandHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
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
        return Result.Success();

    }
}