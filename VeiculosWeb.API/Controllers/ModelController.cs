using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

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
        
        [HttpGet("GetModelsByBrand/{vehicleType}/{brandId:Guid}")]
        [OutputCache(PolicyName = "CacheImmutableResponse", Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetModelsByBrand([FromRoute]VehicleType vehicleType, [FromRoute]Guid brandId)
        {
            var model = await modelService.GetModelsByBrand(vehicleType, brandId);
            return StatusCode(model.Code, model);
        }
    }
}