using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;

namespace CleanArchitecture.Razor.Infrastructure.Security;

public class ClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var winIdentity = (WindowsIdentity)principal.Identity;
        if (winIdentity.Groups != null)
        {
            //-- Getting all the AD groups that user belongs to---  
            foreach (var group in winIdentity.Groups) 
            {
                try
                {
                    var claim = new Claim(winIdentity.RoleClaimType, group.Value);
                    winIdentity.AddClaim(claim);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        return Task.FromResult(principal);
    }
}