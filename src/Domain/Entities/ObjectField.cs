
namespace CleanArchitecture.Razor.Domain.Entities;

public class ObjectField : AuditableEntity, IAuditTrial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ParameterName { get; set; }
    public string Direct { get; set; }
    public string AssociatedType { get; set; }
    public string DataType { get; set; }
    public int? Length { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string SourceTemplateName { get; set; }

  

}
