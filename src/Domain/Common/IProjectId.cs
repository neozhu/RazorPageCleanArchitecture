using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Domain.Common;

public interface IProjectId
{
    int MigrationProjectId { get;set; }
}
