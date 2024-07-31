using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IModelService
    {
        Task<ResponseDTO> GetModelsByBrand(VehicleType vehicleType, Guid brandId);
        Task<ResponseDTO> SyncModels();
    }
}