using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.MappingRules.DTOs;

internal class TempFieldDto
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
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }


}
