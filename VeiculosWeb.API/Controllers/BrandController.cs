using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Service;

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

        [HttpGet("")]
        [ResponseCache(Duration = 86400)]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var brand = await brandService.GetList();
            return StatusCode(brand.Code, brand);
        }
        
        [HttpGet("GetBrands")]
        [ResponseCache(Duration = 86400)]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrands()
        {
            var brand = await brandService.GetBrands();
            return StatusCode(brand.Code, brand);
        }
    }
}