using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces.ToDo;
using CleanArchitecture.Razor.Application.Common.Interfaces.ToDo.DTOs;

namespace CleanArchitecture.Razor.Application.Common.Services;

public class ToDoService : IToDoService
{
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ToDoService> _logger;

    public ToDoService(
         ICurrentUserService currentUser,
        IApplicationDbContext context,
        ILogger<ToDoService> logger
        )
    {
        _currentUser = currentUser;
        _context = context;
        _logger = logger;
    }
    public async Task<ToDoItemDto> AddItem(RequestMappingRuleToDoItem dto, CancellationToken cancellationToken)
    {
        var item = new ToDoItem()
        {
            Title = dto.Title,
            MappingRuleId = dto.MappingRuleId
        };
        await _context.ToDoItems.AddRangeAsync(item);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Add Value Mapping Comments: {dto.Title}, {dto.MappingRuleId}");
        return new ToDoItemDto() { Created = item.Created, CreatedBy = item.CreatedBy, Id = item.Id, IsDone = item.IsDone, Title = item.Title };
    }

    public async Task<ToDoItemDto> AddItem(RequestResultMappingToDoItem dto, CancellationToken cancellationToken)
    {
        var item = new ToDoItem()
        {
            Title = dto.Title,
            ResultMappingId = dto.ResultMappingId
        };
        await _context.ToDoItems.AddRangeAsync(item);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Add Result Mapping Comments: {dto.Title}, {dto.ResultMappingId}");
        return new ToDoItemDto() { Created = item.Created, CreatedBy = item.CreatedBy, Id = item.Id, IsDone = item.IsDone, Title = item.Title };
    }

    public async Task<int> Delete(int id, CancellationToken cancellationToken)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item is not null)
        {
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return item.Id;
    }

    public async Task<int> Done(int id, CancellationToken cancellationToken)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item is not null)
        {
            item.IsDone = !item.IsDone;
            await _context.SaveChangesAsync(cancellationToken);
        }
        return item.Id;
    }

    public async Task<IEnumerable<ToDoItemDto>> GetResultMappingToDoList(int resultmappingid, CancellationToken cancellationToken)
    {
        var list = await _context.ToDoItems.Where(x => x.ResultMappingId == resultmappingid)
              .OrderByDescending(x=>x.Id)
              .Select(x => new ToDoItemDto()
              {
                  Created = x.Created,
                  CreatedBy = x.CreatedBy,
                  Id = x.Id,
                  IsDone = x.IsDone,
                  Title = x.Title
              }).ToListAsync();
        return list;
    }

    public async Task<IEnumerable<ToDoItemDto>> GetToDoList(CancellationToken cancellationToken)
    {
        var currentuser = _currentUser.UserId;
        var data = await _context.ToDoItems.Where(x =>
         (
            (x.MappingRuleId!=null && (x.MappingRule.CreatedBy == currentuser || x.MappingRule.LastModifiedBy == currentuser))
         || (x.ResultMappingId!=null && (x.ResultMapping.CreatedBy == currentuser || x.LastModifiedBy==currentuser))
         )
         && x.IsDone == false)
            .Include(x => x.MappingRule).Include(x => x.ResultMapping)
            .OrderByDescending(x => x.Id)
            .Select(x => new ToDoItemDto()
            {
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                TimeAgo = GetTimeSince(x.Created),
                Title = x.Title,
                Tag= (x.MappingRuleId!=null?"M":"R"),
                Name = (x.MappingRule != null ? x.MappingRule.Name : (x.ResultMapping != null ? x.ResultMapping.Name : null))
            }).ToListAsync();

        return data;
    }
    static string GetTimeSince(DateTime objDateTime)
    {
        // here we are going to subtract the passed in DateTime from the current time converted to UTC
        TimeSpan ts = DateTime.Now.Subtract(objDateTime);
        int intDays = ts.Days;
        int intHours = ts.Hours;
        int intMinutes = ts.Minutes;
        int intSeconds = ts.Seconds;

        if (intDays > 0)
            return string.Format("{0} days", intDays);

        if (intHours > 0)
            return string.Format("{0} hours", intHours);

        if (intMinutes > 0)
            return string.Format("{0} minutes", intMinutes);

        if (intSeconds > 0)
            return string.Format("{0} seconds", intSeconds);

        // let's handle future times..just in case
        if (intDays < 0)
            return string.Format("in {0} days", Math.Abs(intDays));

        if (intHours < 0)
            return string.Format("in {0} hours", Math.Abs(intHours));

        if (intMinutes < 0)
            return string.Format("in {0} minutes", Math.Abs(intMinutes));

        if (intSeconds < 0)
            return string.Format("just now", Math.Abs(intSeconds));

        return "a bit";
    }
    public async Task<IEnumerable<ToDoItemDto>> GetValueMappingToDoList(int mappingruleid, CancellationToken cancellationToken)
    {
        var list = await _context.ToDoItems.Where(x => x.MappingRuleId == mappingruleid)
            .OrderByDescending(x => x.Id)
              .Select(x => new ToDoItemDto()
              {
                  Created = x.Created,
                  CreatedBy = x.CreatedBy,
                  Id = x.Id,
                  IsDone = x.IsDone,
                  Title = x.Title
              }).ToListAsync();
        return list;
    }
}
