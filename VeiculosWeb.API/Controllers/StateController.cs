using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class StateController(IStateService stateService) : BaseController
    {
        [HttpPost("Sync")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> Sync()
        {
            var state = await stateService.SyncStates();
            return StatusCode(state.Code, state);
        }

        [HttpGet("GetStates")]
        [OutputCache(PolicyName = "CacheImmutableResponse", Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetStates()
        {
            var state = await stateService.GetStates();
            return StatusCode(state.Code, state);
        }
    }
}