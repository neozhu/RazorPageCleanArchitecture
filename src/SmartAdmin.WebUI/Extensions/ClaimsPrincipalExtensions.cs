// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        internal static string GetEmail(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.Email);


        internal static string GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.MobilePhone);

        internal static string GetUserId(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

        public static string GetSite(this ClaimsPrincipal claimsPrincipal)
          => claimsPrincipal.FindFirstValue(ClaimTypes.Locality);
        public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
             => claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
    }
}
