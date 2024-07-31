using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.API.Controllers
{
    public class ModelController(IModelService modelService) : BaseController
    {
        [HttpPost("Sync")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> Sync()
        {
            var model = await modelService.SyncModels();
            return StatusCode(model.Code, model);
        }

        [HttpGet("")]
        [ResponseCache(Duration = 86400)]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var model = await modelService.GetList();
            return StatusCode(model.Code, model);
        }
        
        [HttpGet("GetModelsByBrand/{brandId:Guid}")]
        [ResponseCache(Duration = 86400)]
        [AllowAnonymous]
        public async Task<IActionResult> GetModelsByBrand([FromRoute]Guid brandId)
        {
            var model = await modelService.GetModelsByBrand(brandId);
            return StatusCode(model.Code, model);
        }
    }
}