using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Infrastructure.Configurations;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Models;

namespace SmartAdmin.WebUI.EndPoints
{
    [ApiController]
    [Route("api/roles")]
    public class RolesEndpoint : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _manager;
        private readonly SmartSettings _settings;

        public RolesEndpoint(RoleManager<ApplicationRole> manager, SmartSettings settings)
        {
            _manager = manager;
            _settings = settings;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApplicationRole>>> Get()
        {
            var roles = await _manager.Roles.AsNoTracking().ToListAsync();

            return Ok(new { data = roles, recordsTotal = roles.Count, recordsFiltered = roles.Count });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApplicationRole>> Get([FromRoute]string id) => Ok(await _manager.FindByIdAsync(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm]ApplicationRole model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.ConcurrencyStamp = Guid.NewGuid().ToString();

            var result = await _manager.CreateAsync(model);

            if (result.Succeeded)
            {
                return CreatedAtAction("Get", new { id = model.Id }, model);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm]ApplicationRole model)
        {
            var role= await _manager.FindByIdAsync(model.Id);
            if (role != null)
            {
                role.Name = model.Name;
                role.Description = model.Description;

                var result = await _manager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(result);
                }
            }
            

            return BadRequest(IdentityResult.Failed());
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromForm]ApplicationRole model)
        {
            // HACK: The code below is just for demonstration purposes!
            // Please use a different method of preventing the default role from being removed
            if (model.Name == _settings.Theme.Role)
            {
                return BadRequest(SmartError.Failed("Please do not delete the default role! =)"));
            }

            var result = await _manager.DeleteAsync(model);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result);
        }
    }
}
