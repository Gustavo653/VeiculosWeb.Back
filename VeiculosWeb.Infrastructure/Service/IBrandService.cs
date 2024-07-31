using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IBrandService
    {
        Task<ResponseDTO> GetBrands(VehicleType vehicleType);
        Task<ResponseDTO> SyncBrands();
    }
}