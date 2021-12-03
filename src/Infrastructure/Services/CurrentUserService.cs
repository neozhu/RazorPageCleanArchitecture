// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Common.Interfaces;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Http;
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
        var group = UserPrincipal.Current.GetAuthorizationGroups();
        return group.Any(x => roleName.Contains(x.Name));
       
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
    public string DisplayName => UserPrincipal.Current.DisplayName;
    public string UserId => UserPrincipal.Current.SamAccountName;
}
