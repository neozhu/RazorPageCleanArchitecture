// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Common.Interfaces;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Http;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Security.Principal;

namespace CleanArchitecture.Razor.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
 

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor

        )
    {
        _httpContextAccessor = httpContextAccessor;

    }

    public bool IsInRole(params string[] roleName) {
        var identity = _httpContextAccessor.HttpContext.User.Identity as WindowsIdentity;
        var groupNames = from id in identity.Groups
                         select id.Translate(typeof(NTAccount)).Value;
        var group = groupNames.Where(x => x.StartsWith("EURO1\\"))
            .Select(x => x.Split("\\")[1]);
        return group.Any(x => roleName.Contains(x));
       
    }

    
    public int ProjectId() {
        if (_httpContextAccessor.HttpContext?.Request?.Cookies?.TryGetValue("SELECTEDPROJECTID", out string projectId)??false)
        {
            return int.Parse(projectId);
        }
        else
        {
            return 0;
        }

    }
    public string ProjectName()
    {
        if (_httpContextAccessor.HttpContext?.Request?.Cookies?.TryGetValue("SELECTEDPROJECTNAME", out string projectName)??false)
        {
            return projectName;
        }
        else
        {
            return "";
        }

    }

    public IEnumerable<string> GetRoles()
    {
        var identity = _httpContextAccessor.HttpContext.User.Identity as WindowsIdentity;
        var groupNames = from id in identity.Groups
                         select id.Translate(typeof(NTAccount)).Value;
        var group = groupNames.Where(x => x.StartsWith("EURO1\\"))
            .Select(x => x.Split("\\")[1]);
        return group;
    }

    public string DisplayName { get {
            var identity = _httpContextAccessor.HttpContext.User.Identity as WindowsIdentity;
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
        }
    public string UserId => _httpContextAccessor.HttpContext?.User?.Identity?.Name.Split("\\")[1];
}
