using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Driver;
using VeiculosWeb.Infrastructure.Service.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers.Driver
{
    public class DriverChecklistReviewController(IDriverChecklistReviewService checklistReviewService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = $"{nameof(RoleName.Driver)}, {nameof(RoleName.Manager)}")]
        public async Task<IActionResult> CreateChecklistReview([FromForm] DriverChecklistReviewDTO checklistReviewDTO)
        {
            var checklistReview = await checklistReviewService.Create(checklistReviewDTO);
            return StatusCode(checklistReview.Code, checklistReview);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateChecklistReview([FromRoute] Guid id, [FromForm] DriverChecklistReviewDTO checklistReviewDTO)
        {
            var checklistReview = await checklistReviewService.Update(id, checklistReviewDTO);
            return StatusCode(checklistReview.Code, checklistReview);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveChecklistReview([FromRoute] Guid id)
        {
            var checklistReview = await checklistReviewService.Remove(id);
            return StatusCode(checklistReview.Code, checklistReview);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetChecklistReviews([FromQuery] int? takeLast)
        {
            var checklistReview = await checklistReviewService.GetList(takeLast);
            return StatusCode(checklistReview.Code, checklistReview);
        }
    }
}