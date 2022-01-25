using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces.ToDo.DTOs;

namespace CleanArchitecture.Razor.Application.Common.Interfaces.ToDo;

public interface IToDoService:IService
{
    Task<IEnumerable<ToDoItemDto>> GetValueMappingToDoList(int mappingruleid, CancellationToken cancellationToken);
    Task<IEnumerable<ToDoItemDto>> GetResultMappingToDoList(int resultmappingid, CancellationToken cancellationToken);
    Task<ToDoItemDto> AddItem(RequestMappingRuleToDoItem item, CancellationToken cancellationToken);
    Task<ToDoItemDto> AddItem(RequestResultMappingToDoItem item, CancellationToken cancellationToken);
    Task<int> Done(int id, CancellationToken cancellationToken);
    Task<int> Delete(int id, CancellationToken cancellationToken);
}
