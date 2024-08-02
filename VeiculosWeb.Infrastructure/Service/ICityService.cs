using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface ICityService
    {
        Task<ResponseDTO> GetCitiesByState(Guid stateId);
        Task<ResponseDTO> SyncCities();
    }
}