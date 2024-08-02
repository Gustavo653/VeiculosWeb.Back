using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IStateService
    {
        Task<ResponseDTO> GetStates();
        Task<ResponseDTO> SyncStates();
    }
}