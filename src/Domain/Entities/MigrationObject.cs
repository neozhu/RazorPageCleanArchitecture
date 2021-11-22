namespace CleanArchitecture.Razor.Domain.Entities;

public class MigrationObject: AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Team { get; set; }
    public string Description { get; set; }
}
