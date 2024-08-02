using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IFeatureService : IBaseService<FeatureDTO>
    {
        Task<ResponseDTO> GetFeaturesByVehicleType(VehicleType vehicleType);
    }
}