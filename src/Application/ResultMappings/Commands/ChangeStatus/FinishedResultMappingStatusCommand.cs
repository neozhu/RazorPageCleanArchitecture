using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.ChangeStatus;

public record FinishedResultMappingStatusCommand: IRequest<Result>
{
    public int Id { get; set; }
}

public class FinishedResultMappingStatusCommandHandler :
    IRequestHandler<FinishedResultMappingStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<FinishedResultMappingStatusCommandHandler> _logger;

    public FinishedResultMappingStatusCommandHandler(
        IApplicationDbContext context,
        ILogger<FinishedResultMappingStatusCommandHandler> logger
        )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(FinishedResultMappingStatusCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.ResultMappings.FindAsync(request.Id);
        if (item == null)
        {
            return Result.Failure(new string[] { $"Not found value mapping rule." });
        }
        item.Status = "Finished";
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Set the status of result mapping to finished:{@Request}",request);
        return Result.Success();

    }
}