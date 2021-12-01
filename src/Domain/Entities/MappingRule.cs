namespace CleanArchitecture.Razor.Domain.Entities;

#nullable disable
public class MappingRule:AuditableEntity, IAuditTrial, IProjectId
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string LegacyField1 { get; set; }
    public string ImportParameterField1 { get; set; }
    public string LegacyDescription1 { get; set; }
    public string LegacyField2 { get; set; }
    public string ImportParameterField2 { get; set; }
    public string LegacyDescription2 { get; set; }
    public string LegacyField3 { get; set; }
    public string ImportParameterField3 { get; set; }
    public string LegacyDescription3 { get; set; }
    public string LegacyField4 { get; set; }
    public string ImportParameterField4 { get; set; }
    public string LegacyDescription4 { get; set; }
    public string NewValueField { get; set; }
    public string ExportParameterField { get; set; }
    public string NewValueFieldDescription { get; set; }
    public bool IsMock { get; set; }
    public string LegacySystem { get; set; }
    public string ProjectName { get; set; }
    public string RelevantObjects { get; set; }
    public string Team { get; set; }
    public string Comments { get; set; }
    public string TemplateFile { get; set; }
    public string TemplateDescription { get; set; }

    public string Active { get; set; } = "Active";

    public virtual ICollection<FieldMappingValue> FieldMappingValues { get; set; } = new HashSet<FieldMappingValue>();

    public int MigrationProjectId { get; set; }
    public virtual MigrationProject MigrationProject { get; set; }
}
