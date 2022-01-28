using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Common.Interfaces.ToDo.DTOs;

public class ToDoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public string Name { get; set; }
    public string TimeAgo { get; set; }
    public string Tag { get; set; }
}

public class RequestMappingRuleToDoItem
{
    public string Title { get; set; }
    public int? MappingRuleId { get; set; }
}
public class RequestResultMappingToDoItem
{
    public string Title { get; set; }
    public int? ResultMappingId { get; set; }
}
