using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.ChangeStatus;

public record VerifyResultMappingStatusCommand: IRequest<Result>
{
    public int[] Id { get; set; }
}

public class VerifyResultMappingStatusCommandHandler :
    IRequestHandler<VerifyResultMappingStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<VerifyResultMappingStatusCommandHandler> _logger;

    public VerifyResultMappingStatusCommandHandler(
        IApplicationDbContext context,
        ILogger<VerifyResultMappingStatusCommandHandler> logger
        )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(VerifyResultMappingStatusCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.ResultMappingDatas.Where(x=>request.Id.Contains(x.Id)).ToListAsync();
        var resultId = items[0].ResultMappingId;
        foreach (var item in items)
        {
            item.Verify = "Verified";
            item.VerifiedDate = DateTime.UtcNow;
            _context.ResultMappingDatas.Update(item);    
        }
       
        await _context.SaveChangesAsync(cancellationToken);

        var count = await _context.ResultMappingDatas.CountAsync(x=>x.ResultMappingId==resultId && x.Verify== "Verified");
        var mapping = await _context.ResultMappings.FindAsync(resultId);
        mapping.Verified = count;
        mapping.Status = "Ongoing";
        _context.ResultMappings.Update(mapping);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Set the status of result mapping data to verified:{@Request}",request);
        return Result.Success();

    }
}