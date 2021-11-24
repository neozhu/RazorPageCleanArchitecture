namespace CleanArchitecture.Razor.Domain.Entities;

public class FieldMappingValue : AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public int MappingRuleId { get; set; }
    public virtual MappingRule MappingRule { get; set; }
    public string Mock { get; set; }
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
