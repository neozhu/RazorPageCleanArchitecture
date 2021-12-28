using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.ChangeStatus;

public record ScopedResultMappingStatusCommand : IRequest<Result>
{
    public int[] Id { get; set; }
}

public class ScopedResultMappingStatusCommandHandler :
    IRequestHandler<ScopedResultMappingStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _userService;
    private readonly ILogger<ScopedResultMappingStatusCommand> _logger;

    public ScopedResultMappingStatusCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService userService,
        ILogger<ScopedResultMappingStatusCommand> logger
        )
    {
        _context = context;
        _userService = userService;
        _logger = logger;
    }

    public async Task<Result> Handle(ScopedResultMappingStatusCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.ResultMappingDatas.Where(x=>request.Id.Contains(x.Id)).ToListAsync();
        var resultId = items[0].ResultMappingId;
        foreach (var item in items)
        {
            if(item.Verify== "Unselected")
            {
                item.Verify = "Selected";
                item.Owner = _userService.UserId;
            }
            else if(item.Verify == "Selected")
            {
                item.Verify = "Unselected";
                item.Owner = null;
            }
            
            //item.VerifiedDate = DateTime.UtcNow;
            _context.ResultMappingDatas.Update(item);    
        }
       
        await _context.SaveChangesAsync(cancellationToken);

        var count = await _context.ResultMappingDatas.CountAsync(x => x.ResultMappingId == resultId && x.Verify == "Verified");
        var total = await _context.ResultMappingDatas.CountAsync(x => x.ResultMappingId == resultId && (x.Verify == "Verified" || x.Verify == "Selected"));
        var mapping = await _context.ResultMappings.FindAsync(resultId);
        mapping.Verified = count;
        mapping.Total = total;
        if(count == total)
        {
            mapping.Status = "Finished";
        }
        else
        {
            mapping.Status = "Ongoing";
        }
        _context.ResultMappings.Update(mapping);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("select/unselect the verify status:{@Request}", request);
        return Result.Success();

    }
}