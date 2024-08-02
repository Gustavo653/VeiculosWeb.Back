using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class FuelController(IFuelService fuelService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> CreateFuel([FromBody] FuelDTO fuelDTO)
        {
            var fuel = await fuelService.Create(fuelDTO);
            return StatusCode(fuel.Code, fuel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> UpdateFuel([FromRoute] Guid id, [FromBody] FuelDTO fuelDTO)
        {
            var fuel = await fuelService.Update(id, fuelDTO);
            return StatusCode(fuel.Code, fuel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> RemoveFuel([FromRoute] Guid id)
        {
            var fuel = await fuelService.Remove(id);
            return StatusCode(fuel.Code, fuel);
        }

        [HttpGet("")]
        [OutputCache(PolicyName = "CacheImmutableResponse", Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetFuels()
        {
            var fuel = await fuelService.GetList();
            return StatusCode(fuel.Code, fuel);
        }
    }
}