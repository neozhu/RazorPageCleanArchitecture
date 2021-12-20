using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Domain.Entities;

public  class ResultMappingData : AuditableEntity, IAuditTrial, IProjectId
{
    public int Id { get; set; }
    public int MigrationProjectId { get; set; }
    public int ResultMappingId { get; set;}
    public virtual ResultMapping ResultMapping { get; set; }
    public Dictionary<string,string> FieldData { get; set; }= new Dictionary<string,string>();  
    public string Verify { get; set; } = "Unset";
    public string Owner { get; set; }
    public string Scoped { get; set; }
    public DateTime? VerifiedDate { get; set; }
    public string Comments { get; set; }

    public string Field1 { get; set; }
    public string Field2 { get; set; }
    public string Field3 { get; set; }
    public string Field4 { get; set; }
    public string Field5 { get; set; }
    public string Field6 { get; set; }
    public string Field7 { get; set; }
    public string Field8 { get; set; }
    public string Field9 { get; set; }
    public string Field10 { get; set; }
    public string Field11 { get; set; }
    public string Field12 { get; set; }
    public string Field13 { get; set; }
    public string Field14 { get; set; }
    public string Field15 { get; set; }
    public string Field16 { get; set; }
    public string Field17 { get; set; }
    public string Field18 { get; set; }
    public string Field19 { get; set; }
    public string Field20 { get; set; }

}

