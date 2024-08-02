using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class ColorController(IColorService colorService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> CreateColor([FromBody] ColorDTO colorDTO)
        {
            var color = await colorService.Create(colorDTO);
            return StatusCode(color.Code, color);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> UpdateColor([FromRoute] Guid id, [FromBody] ColorDTO colorDTO)
        {
            var color = await colorService.Update(id, colorDTO);
            return StatusCode(color.Code, color);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> RemoveColor([FromRoute] Guid id)
        {
            var color = await colorService.Remove(id);
            return StatusCode(color.Code, color);
        }

        [HttpGet("")]
        [OutputCache(PolicyName = "CacheImmutableResponse", Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetColors()
        {
            var color = await colorService.GetList();
            return StatusCode(color.Code, color);
        }
    }
}