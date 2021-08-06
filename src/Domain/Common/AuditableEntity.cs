using System;

namespace CleanArchitecture.Razor.Domain.Common
{
    public interface IEntity {
       
     }
    public abstract class AuditableEntity:IEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
