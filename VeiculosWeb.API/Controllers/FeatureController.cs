using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.API.Controllers
{
    public class FeatureController(IFeatureService featureService) : BaseController
    {
        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> CreateFeature([FromBody] FeatureDTO featureDTO)
        {
            var feature = await featureService.Create(featureDTO);
            return StatusCode(feature.Code, feature);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> UpdateFeature([FromRoute] Guid id, [FromBody] FeatureDTO featureDTO)
        {
            var feature = await featureService.Update(id, featureDTO);
            return StatusCode(feature.Code, feature);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Admin))]
        public async Task<IActionResult> RemoveFeature([FromRoute] Guid id)
        {
            var feature = await featureService.Remove(id);
            return StatusCode(feature.Code, feature);
        }

        [HttpGet("GetFeaturesByVehicleType/{vehicleType}")]
        [OutputCache(PolicyName = "CacheImmutableResponse", Duration = Consts.CacheTimeout)]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeaturesByVehicleType([FromRoute] VehicleType vehicleType)
        {
            var feature = await featureService.GetFeaturesByVehicleType(vehicleType);
            return StatusCode(feature.Code, feature);
        }
    }
}