// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Razor.Application.Common.Interfaces.Identity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SmartAdmin.WebUI.EndPoints
{
    [ApiController]
    [Route("api/identity/token")]
    public class IdentityEndpoint : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityEndpoint(
            IIdentityService identityService
            )
        {
            _identityService = identityService;
        }
        /// <summary>
        /// Get Token (Email, Password)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost]
        public async Task<ActionResult> Get([FromBody]TokenRequestDto model)
        {
            var response = await _identityService.LoginAsync(model);
            return Ok(response);
        }
        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequestDto model)
        {
            var response = await _identityService.RefreshTokenAsync(model);
            return Ok(response);
        }

    }
}
