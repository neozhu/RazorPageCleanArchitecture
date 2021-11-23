

namespace CleanArchitecture.Razor.Domain.Entities;

public class FieldValueMapping : AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public int ObjectFieldId { get; set; }
    public virtual ObjectField ObjectField { get; set; }
    public string Stage { get; set; }
    public string Legacy1 { get; set; }
    public string Legacy2 { get; set; }
    public string Legacy3 { get; set; }
    public string Legacy4 { get; set; }
    public string NewValue { get; set; }
    public string LegacySystem { get; set; }
    public string Description { get; set; }
    public string Team { get; set; }
    public string Check { get; set; }
    public string Comments { get; set; }


}
