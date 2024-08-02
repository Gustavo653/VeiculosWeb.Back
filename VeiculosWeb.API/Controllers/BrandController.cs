using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class BrandController(IBrandService brandService) : BaseController
    {
        [HttpPost("Sync")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> Sync()
        {
            var brand = await brandService.SyncBrands();
            return StatusCode(brand.Code, brand);
        }

        [HttpGet("GetBrands/{vehicleType}")]
        [OutputCache(PolicyName = "CacheImmutableResponse", Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrands([FromRoute] VehicleType vehicleType)
        {
            var brand = await brandService.GetBrands(vehicleType);
            return StatusCode(brand.Code, brand);
        }
    }
}