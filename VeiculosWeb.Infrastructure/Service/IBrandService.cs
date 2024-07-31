using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IBrandService
    {
        Task<ResponseDTO> GetList();
        Task<ResponseDTO> SyncBrands();
    }
}