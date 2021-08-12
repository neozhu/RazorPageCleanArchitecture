// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace SmartAdmin.WebUI.Services
{
    public class ApplicationClaimsIdentityFactory : Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory<ApplicationUser>
    {
        UserManager<ApplicationUser> _userManager;
        public ApplicationClaimsIdentityFactory(UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }
        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            if (!string.IsNullOrEmpty(user.Site))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.Locality, user.Site)
            });
            }
            if (!string.IsNullOrEmpty(user.DisplayName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            });
            }
            return principal;
        }
    }
}
