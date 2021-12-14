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

    public FinishedResultMappingStatusCommandHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
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
        return Result.Success();

    }
}