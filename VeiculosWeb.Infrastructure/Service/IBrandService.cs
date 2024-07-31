using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IBrandService
    {
        Task<ResponseDTO> GetList();
        Task<ResponseDTO> SyncBrands();
    }
}