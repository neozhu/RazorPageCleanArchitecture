using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Infrastructure.Identity
{
 public  class ApplicationRoleClaim:IdentityRoleClaim<string>
  {
    public string Description { get; set; }
    public string Group { get; set; }
    public virtual ApplicationRole Role { get; set; }

    public ApplicationRoleClaim() : base()
    {
    }

    public ApplicationRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
    {
      Description = roleClaimDescription;
      Group = roleClaimGroup;
    }
  }
}
