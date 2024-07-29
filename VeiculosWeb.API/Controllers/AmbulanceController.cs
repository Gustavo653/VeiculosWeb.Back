using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers
{
    public class AmbulanceController(IAmbulanceService ambulanceService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> CreateAmbulance([FromBody] AmbulanceDTO ambulanceDTO)
        {
            var ambulance = await ambulanceService.Create(ambulanceDTO);
            return StatusCode(ambulance.Code, ambulance);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateAmbulance([FromRoute] Guid id, [FromBody] AmbulanceDTO ambulanceDTO)
        {
            var ambulance = await ambulanceService.Update(id, ambulanceDTO);
            return StatusCode(ambulance.Code, ambulance);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveAmbulance([FromRoute] Guid id)
        {
            var ambulance = await ambulanceService.Remove(id);
            return StatusCode(ambulance.Code, ambulance);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAmbulances()
        {
            var ambulance = await ambulanceService.GetList();
            return StatusCode(ambulance.Code, ambulance);
        }
    }
}