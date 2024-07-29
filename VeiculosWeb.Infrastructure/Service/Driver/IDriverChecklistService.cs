using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Driver;
using VeiculosWeb.Infrastructure.Base;

namespace VeiculosWeb.Infrastructure.Service.Driver
{
    public interface IDriverChecklistService : IBaseService<DriverChecklistDTO>
    {
        Task<ResponseDTO> GetById(Guid id);
    }
}