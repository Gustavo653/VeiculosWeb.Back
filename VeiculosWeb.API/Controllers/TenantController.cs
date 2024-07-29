using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers
{
    public class TenantController(ITenantService tenantService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> CreateTenant([FromBody] BasicDTO name)
        {
            var item = await tenantService.Create(name);
            return StatusCode(item.Code, item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> UpdateTenant([FromRoute] Guid id, [FromBody] BasicDTO name)
        {
            var item = await tenantService.Update(id, name);
            return StatusCode(item.Code, item);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> RemoveTenant([FromRoute] Guid id)
        {
            var item = await tenantService.Remove(id);
            return StatusCode(item.Code, item);
        }

        [HttpGet("")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> GetTenants()
        {
            var item = await tenantService.GetList();
            return StatusCode(item.Code, item);
        }
    }
}