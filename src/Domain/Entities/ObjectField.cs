
namespace CleanArchitecture.Razor.Domain.Entities;

public class ObjectField : AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string MasterDataRelevant { get; set; }
    public string TechMockMasterData { get; set; }
    public string Team { get; set; }
    public string Status { get; set; }
    public string Link { get; set; }
    public string LegacySystem { get; set; }
    public string IsUsedAK1 { get; set; }
    public string MajorTable{get;set;}
    public string Cases { get; set; }
    public string Numbers { get; set; }
    public string RelevantObjects { get; set; }
    public string Check { get; set; }
    public string Comments { get; set; }

    public string MigrationTemplate { get; set; }

    public virtual ICollection<FieldValueMapping> FieldValueMappings { get; set; } = new HashSet<FieldValueMapping>();

}
