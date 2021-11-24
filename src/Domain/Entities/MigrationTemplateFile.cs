namespace CleanArchitecture.Razor.Domain.Entities;

public class MigrationTemplateFile:AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ObjectField { get; set; }
    public string Legacy1Field { get; set; }
    public string Legacy2Field { get; set; }
    public string Legacy3Field { get; set; }
    public string NewValueField { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public string LegacySystem { get; set; }
    public string ProjectName { get; set; }
    public string Comments { get; set; }
}
