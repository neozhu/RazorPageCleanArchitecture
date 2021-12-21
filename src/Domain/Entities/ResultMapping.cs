namespace CleanArchitecture.Razor.Domain.Entities;

public class ResultMapping : AuditableEntity, IAuditTrial, IProjectId
{
    public int Id { get; set; }
    public int MigrationProjectId { get; set; }
    public virtual MigrationProject MigrationProject { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string RelevantMock { get; set; }
    public string LegacySystem { get; set; }
    public string ProjectName { get; set; }
    public string RelevantObjects { get; set; }
    public string Team { get; set; }
    public string MigrationApproach { get; set; }
    public string TemplateFile { get; set; }
    public string TemplateDescription { get; set; }
    public int Verified { get; set; }
    public int Total { get; set; }
    public List<FieldParameter> FieldParameters { get; set; }=new List<FieldParameter>();
    //public virtual ICollection<ResultMappingData> ResultMappingDatas { get; set; } = new HashSet<ResultMappingData>();
    

}

public class FieldParameter
{
    public string FieldName { get; set; }
    public string DataElement { get; set; }
    public string Direction { get; set; }
    public string Description { get; set; }
}
