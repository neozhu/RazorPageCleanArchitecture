
namespace CleanArchitecture.Razor.Domain.Entities;

public class ObjectField : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool MasterDataRelevant { get; set; }
    public bool TechMockMasterData { get; set; }
    public string Team { get; set; }

    public string Status { get; set; }
    public string Link { get; set; }
    public string LegacySystem { get; set; }
    public bool IsUsedAK1 { get; set; }
    public string MajorTable{get;set;}
    public string Cases { get; set; }
    public string Numbers { get; set; }
    public string RelevantObjects { get; set; }
    public string Check { get; set; }
    public string Comments { get; set; }
}
