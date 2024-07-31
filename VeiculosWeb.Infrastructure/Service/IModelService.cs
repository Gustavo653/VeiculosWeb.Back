using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IModelService
    {
        Task<ResponseDTO> GetList();
        Task<ResponseDTO> SyncModels();
    }
}