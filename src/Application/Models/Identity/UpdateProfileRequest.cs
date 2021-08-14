// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Models.Identity
{
    public class UpdateProfileRequest
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Site { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProfilePictureDataUrl { get; set; }
    }
}
