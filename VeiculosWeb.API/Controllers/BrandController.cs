using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers
{
    public class BrandController(IBrandService brandService) : BaseController
    {
        [HttpPost("Sync")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> Sync()
        {
            var fuel = await brandService.SyncBrands();
            return StatusCode(fuel.Code, fuel);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrands()
        {
            var fuel = await brandService.GetList();
            return StatusCode(fuel.Code, fuel);
        }
    }
}