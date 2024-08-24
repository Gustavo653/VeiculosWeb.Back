using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class CityController(ICityService cityService) : BaseController
    {
        [HttpPost("Sync")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> Sync()
        {
            var city = await cityService.SyncCities();
            return StatusCode(city.Code, city);
        }

        [HttpGet("GetCitiesByState/{stateId:Guid}")]
        [OutputCache(PolicyName = Consts.CacheName, Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCitiesByState([FromRoute] Guid stateId)
        {
            var city = await cityService.GetCitiesByState(stateId);
            return StatusCode(city.Code, city);
        }
    }
}