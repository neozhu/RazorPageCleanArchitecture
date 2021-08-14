// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Models.Identity;

namespace CleanArchitecture.Razor.Application.Common.Interfaces.Identity
{
    public interface IUserService : IService
    {
        Task<IResult<IEnumerable<ApplicationUserDto>>> GetAllAsync();
        Task<IResult<IEnumerable<ApplicationUserDto>>> GetInRoleAsync(string roleName);
        Task<int> GetCountAsync();
        Task<IResult<ApplicationUserDto>> GetAsync(string userId);
        Task<IResult> RegisterAsync(RegisterRequest request, string origin);
        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);
        Task<IResult<UserRolesDto>> GetRolesAsync(string id);
        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);
        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);
        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
        Task<byte[]> ExportToExcelAsync(string searchString = "");
    }
}
