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
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ToDoService> _logger;

    public ToDoService(
        IApplicationDbContext context,
        ILogger<ToDoService> logger
        )
    {
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
