using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Paramedic;
using VeiculosWeb.Infrastructure.Base;

namespace VeiculosWeb.Infrastructure.Service.Paramedic
{
    public interface IParamedicChecklistService : IBaseService<ParamedicChecklistDTO>
    {
        Task<ResponseDTO> GetById(Guid id);
    }
}