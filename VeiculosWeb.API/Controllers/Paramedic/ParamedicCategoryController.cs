using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Service.Paramedic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers.Paramedic
{
    public class ParamedicCategoryController(IParamedicCategoryService categoryService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> CreateCategory([FromBody] BasicDTO name)
        {
            var category = await categoryService.Create(name);
            return StatusCode(category.Code, category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] BasicDTO name)
        {
            var category = await categoryService.Update(id, name);
            return StatusCode(category.Code, category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveCategory([FromRoute] Guid id)
        {
            var category = await categoryService.Remove(id);
            return StatusCode(category.Code, category);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCategories()
        {
            var category = await categoryService.GetList();
            return StatusCode(category.Code, category);
        }
    }
}