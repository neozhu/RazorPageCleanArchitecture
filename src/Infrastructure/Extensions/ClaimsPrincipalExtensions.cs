// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using CleanArchitecture.Razor.Infrastructure.Constants.ClaimTypes;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.DirectoryServices;

namespace CleanArchitecture.Razor.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as WindowsIdentity;
        var domain = identity.Name.Split("\\")[0];
        var name = identity.Name.Split("\\")[1];
        var emailaddress = "";
        using (var entry = new DirectoryEntry($"LDAP://{domain}"))
        {
            using (var searcher = new DirectorySearcher(entry))
            {
                searcher.Filter = $"(sAMAccountName={name})";
                searcher.PropertiesToLoad.Add("mail");
                var searchResult = searcher.FindOne();
                if (searchResult != null && searchResult.Properties.Contains("mail"))
                {
                    emailaddress = (string)searchResult.Properties["mail"][0];
                }
                else
                {
                    // user not found
                }
            }
        }


        return emailaddress;
    }


    public static string GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as WindowsIdentity;
        var domain = identity.Name.Split("\\")[0];
        var name = identity.Name.Split("\\")[1];
        var phoneNumber = "";
        using (var entry = new DirectoryEntry($"LDAP://{domain}"))
        {
            using (var searcher = new DirectorySearcher(entry))
            {
                searcher.Filter = $"(sAMAccountName={name})";
                searcher.PropertiesToLoad.Add("telephoneNumber");
                var searchResult = searcher.FindOne();
                if (searchResult != null && searchResult.Properties.Contains("telephoneNumber"))
                {
                    phoneNumber = (string)searchResult.Properties["telephoneNumber"][0];
                }
                else
                {
                    // user not found
                }
            }
        }
        return phoneNumber;
    }

    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as WindowsIdentity;
        return identity.Name.Split("\\")[1];
    }

    public static string GetSite(this ClaimsPrincipal claimsPrincipal)
      => claimsPrincipal.FindFirstValue(ClaimTypes.Locality);
    //public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
    //     => claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
    public static string GetProfilePictureDataUrl(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ApplicationClaimTypes.ProfilePictureDataUrl);


    public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
    {

        var identity = claimsPrincipal.Identity as WindowsIdentity;
        if(identity==null)  return string.Empty;
        var domain = identity.Name.Split("\\")[0];
        var displayname = identity.Name.Split("\\")[1];
        using (var entry = new DirectoryEntry($"LDAP://{domain}"))
        {
            using (var searcher = new DirectorySearcher(entry))
            {
                searcher.Filter = $"(sAMAccountName={displayname})";
                searcher.PropertiesToLoad.Add("displayName");
                var searchResult = searcher.FindOne();
                if (searchResult != null && searchResult.Properties.Contains("displayName"))
                {
                    displayname = (string)searchResult.Properties["displayName"][0];
                }
                else
                {
                    // user not found
                }
            }
        }


        return displayname;
    }
    public static string GetDefaultGroup(this ClaimsPrincipal claimsPrincipal)
    {

        var identity = claimsPrincipal.Identity as WindowsIdentity;
        if (identity == null) return string.Empty;
        var groupNames = from id in identity.Groups
                         select id.Translate(typeof(NTAccount)).Value;
        var group = groupNames.Where(x => x.StartsWith("EURO1\\"))
            .Select(x => x.Split("\\")[1]);

        if (group.Any(x => x.ToUpper().Contains("MOVE-VT-MIG")))
        {
            return group.First(x => x.ToUpper().Contains("MOVE-VT-MIG"));
        }else if(group.Any(x => x.ToUpper().Contains("MIG")))
        {
            return group.First(x => x.ToUpper().Contains("MIG"));
        }
        else if (group.Any(x => x.ToUpper().Contains("DSC IT SAP")))
        {
            return group.First(x => x.ToUpper().Contains("DSC IT SAP"));
        }

        return group.First();
    }

    public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as WindowsIdentity;
        if (identity == null) return Array.Empty<string>();
        var groupNames = from id in identity.Groups
                         select id.Translate(typeof(NTAccount)).Value;
        var group = groupNames.Where(x => x.StartsWith("EURO1\\"))
            .Select(x => x.Split("\\")[1]);
        return group.ToArray();
    }
}

