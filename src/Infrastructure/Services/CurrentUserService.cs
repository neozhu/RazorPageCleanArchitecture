// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
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

    public bool IsInRole(string roleName) {
        var groups = new List<string>();
        var wi = (WindowsIdentity)_httpContextAccessor.HttpContext.User.Identity;
        if (wi.Groups != null)
        {
            foreach (var group in wi.Groups)
            {
                try
                {
                    groups.Add(group.Translate(typeof(NTAccount)).ToString());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        return groups.Any(x=>x==roleName);
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
    public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
}
