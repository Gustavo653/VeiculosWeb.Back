using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Service.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers.Driver
{
    public class DriverItemController(IDriverItemService itemService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> CreateItem([FromBody] BasicDTO name)
        {
            var item = await itemService.Create(name);
            return StatusCode(item.Code, item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, [FromBody] BasicDTO name)
        {
            var item = await itemService.Update(id, name);
            return StatusCode(item.Code, item);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveItem([FromRoute] Guid id)
        {
            var item = await itemService.Remove(id);
            return StatusCode(item.Code, item);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetItems()
        {
            var item = await itemService.GetList();
            return StatusCode(item.Code, item);
        }
    }
}