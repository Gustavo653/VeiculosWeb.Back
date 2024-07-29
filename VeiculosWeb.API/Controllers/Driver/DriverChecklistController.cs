using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Driver;
using VeiculosWeb.Infrastructure.Service.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers.Driver
{
    public class DriverChecklistController(IDriverChecklistService checklistService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> CreateChecklist([FromBody] DriverChecklistDTO checklistDTO)
        {
            var checklist = await checklistService.Create(checklistDTO);
            return StatusCode(checklist.Code, checklist);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateChecklist([FromRoute] Guid id, [FromBody] DriverChecklistDTO checklistDTO)
        {
            var checklist = await checklistService.Update(id, checklistDTO);
            return StatusCode(checklist.Code, checklist);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveChecklist([FromRoute] Guid id)
        {
            var checklist = await checklistService.Remove(id);
            return StatusCode(checklist.Code, checklist);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetChecklists()
        {
            var checklist = await checklistService.GetList();
            return StatusCode(checklist.Code, checklist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var checklist = await checklistService.GetById(id);
            return StatusCode(checklist.Code, checklist);
        }
    }
}