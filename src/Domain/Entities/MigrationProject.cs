using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Domain.Entities;

public  class MigrationProject: AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime BeginDateTime { get; set; }
    public DateTime? FinishedDateTime { get; set; }
    public int Progress { get; set; }
    public string Description { get; set; }

    public virtual ICollection<MigrationObject> MigrationObjects { get; set; } = new HashSet<MigrationObject>();
}
