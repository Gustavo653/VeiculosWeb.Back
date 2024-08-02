using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class GearboxController(IGearboxService gearboxService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> CreateGearbox([FromBody] GearboxDTO gearboxDTO)
        {
            var gearbox = await gearboxService.Create(gearboxDTO);
            return StatusCode(gearbox.Code, gearbox);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> UpdateGearbox([FromRoute] Guid id, [FromBody] GearboxDTO gearboxDTO)
        {
            var gearbox = await gearboxService.Update(id, gearboxDTO);
            return StatusCode(gearbox.Code, gearbox);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> RemoveGearbox([FromRoute] Guid id)
        {
            var gearbox = await gearboxService.Remove(id);
            return StatusCode(gearbox.Code, gearbox);
        }

        [HttpGet("")]
        [OutputCache(PolicyName = "CacheImmutableResponse", Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetGearboxes()
        {
            var gearbox = await gearboxService.GetList();
            return StatusCode(gearbox.Code, gearbox);
        }
    }
}