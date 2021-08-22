// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Infrastructure.Constants.ClaimTypes;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace SmartAdmin.WebUI.Services
{
    public class ApplicationClaimsIdentityFactory : Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory<ApplicationUser>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationClaimsIdentityFactory(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            
            if (!string.IsNullOrEmpty(user.Site))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.Locality, user.Site)
            });
            }
            if (!string.IsNullOrEmpty(user.ProfilePictureDataUrl))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ApplicationClaimTypes.ProfilePictureDataUrl, user.ProfilePictureDataUrl)
            });
            }
            if (!string.IsNullOrEmpty(user.DisplayName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            });
            }
            var appuser = await _userManager.FindByIdAsync(user.Id);
            var roles = await _userManager.GetRolesAsync(appuser);
            foreach(var rolename in roles)
            {
                var role = await _roleManager.FindByNameAsync(rolename);
                var claims = await _roleManager.GetClaimsAsync(role);
                foreach(var claim in claims)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaim(claim);
                }

            }
            return principal;
        }
    }
}
