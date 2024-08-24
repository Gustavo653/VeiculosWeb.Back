using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class CarController(ICarService carService) : BaseController
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateCar([FromBody] BaseVehicleDTO baseVehicleDTO)
        {
            var color = await carService.Create(baseVehicleDTO);
            return StatusCode(color.Code, color);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCar([FromRoute] Guid id, [FromBody] BaseVehicleDTO baseVehicleDTO)
        {
            var color = await carService.Update(id, baseVehicleDTO);
            return StatusCode(color.Code, color);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveCar([FromRoute] Guid id)
        {
            var color = await carService.Remove(id);
            return StatusCode(color.Code, color);
        }

        [HttpGet("")]
        [OutputCache(PolicyName = Consts.CacheName, Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCars()
        {
            var color = await carService.GetList();
            return StatusCode(color.Code, color);
        }
    }
}