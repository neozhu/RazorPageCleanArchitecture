// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Models.Identity
{
    public class RegisterRequest
    {
        public string Site { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool AgreeToTerms { get; set; }
        public bool SignUp { get; set; }
        public string ProfilePictureDataUrl { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
    }
}
