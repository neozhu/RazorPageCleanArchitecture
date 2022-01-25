using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Domain.Entities;

public class ToDoItem : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public int? MappingRuleId { get; set; }
    public virtual MappingRule MappingRule { get; set; }
    public int? ResultMappingId { get; set; }
    public virtual ResultMapping ResultMapping { get; set; }
}
