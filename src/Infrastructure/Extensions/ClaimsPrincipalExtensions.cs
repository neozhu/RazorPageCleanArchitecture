// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using CleanArchitecture.Razor.Infrastructure.Constants.ClaimTypes;
using System.Security.Principal;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace CleanArchitecture.Razor.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{

    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
      => claimsPrincipal.FindFirstValue(ClaimTypes.Email);


    public static string GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
    => claimsPrincipal.FindFirstValue(ClaimTypes.MobilePhone);

    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    => claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

    public static string GetSite(this ClaimsPrincipal claimsPrincipal)
      => claimsPrincipal.FindFirstValue(ClaimTypes.Locality);
    //public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
    //     => claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
    public static string GetProfilePictureDataUrl(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ApplicationClaimTypes.ProfilePictureDataUrl);


    public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
    => claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
    public static string GetDefaultGroup(this ClaimsPrincipal claimsPrincipal)
     => claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();

    public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal)
     => claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
}

