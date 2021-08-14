// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Models.Identity
{
    public class UserRolesDto
    {
        public List<UserRoleDto> UserRoles { get; set; } = new();
    }
    public class UserRoleDto
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool Selected { get; set; }
    }
}
