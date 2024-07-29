using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Driver;
using VeiculosWeb.Infrastructure.Base;

namespace VeiculosWeb.Infrastructure.Service.Driver
{
    public interface IDriverChecklistReviewService : IBaseService<DriverChecklistReviewDTO>
    {
        Task<ResponseDTO> GetList(int? takeLast);
    }
}