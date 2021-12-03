// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using CleanArchitecture.Razor.Infrastructure.Constants.ClaimTypes;
using System.DirectoryServices.AccountManagement;
namespace CleanArchitecture.Razor.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        => UserPrincipal.Current.EmailAddress;


    public static string GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
        => UserPrincipal.Current.VoiceTelephoneNumber;

    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
       => UserPrincipal.Current.SamAccountName;

    public static string GetSite(this ClaimsPrincipal claimsPrincipal)
      => claimsPrincipal.FindFirstValue(ClaimTypes.Locality);
    //public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
    //     => claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
    public static string GetProfilePictureDataUrl(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ApplicationClaimTypes.ProfilePictureDataUrl);


    public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
      => UserPrincipal.Current.DisplayName;
    public static string GetDefaultGroup(this ClaimsPrincipal claimsPrincipal)
    {
        var group = UserPrincipal.Current.GetAuthorizationGroups();
        if (group.Any(x => x.Name.Contains("SAP")))
        {
            return group.First(x => x.Name.Contains("SAP")).Name;
        }
        return group.First().DisplayName;
    }

    public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal)
      => claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
}

