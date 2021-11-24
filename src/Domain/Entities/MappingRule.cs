namespace CleanArchitecture.Razor.Domain.Entities;

public class MappingRule:AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string LegacyField1 { get; set; }
    public string LegacyDescription1 { get; set; }
    public string LegacyField2 { get; set; }
    public string LegacyDescription2 { get; set; }
    public string LegacyField3 { get; set; }
    public string LegacyDescription3 { get; set; }
    public string LegacyField4 { get; set; }
    public string LegacyDescription4 { get; set; }
    public string NewValueField { get; set; }
    public string NewValueFieldDescription { get; set; }
    public string IsMock { get; set; }
    public string LegacySystem { get; set; }
    public string ProjectName { get; set; }
    public string ObjectName { get; set; }
    public string Team { get; set; }
    public string Comments { get; set; }
    public string TemplateFile { get; set; }
}
