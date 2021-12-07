// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }
    string DisplayName { get; }
    bool IsInRole(params string[] roleName);
    int ProjectId();
    string ProjectName();
}
