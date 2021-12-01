
namespace CleanArchitecture.Razor.Domain.Entities;

public class ObjectField : AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MigrationProjectId { get; set; }
    public virtual MigrationProject MigrationProject { get; set; }

}
