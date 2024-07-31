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
            var fuel = await modelService.SyncModels();
            return StatusCode(fuel.Code, fuel);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetModels()
        {
            var fuel = await modelService.GetList();
            return StatusCode(fuel.Code, fuel);
        }
    }
}