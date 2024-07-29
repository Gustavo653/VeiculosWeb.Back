using VeiculosWeb.DTO.Base;
using VeiculosWeb.DTO.Paramedic;
using VeiculosWeb.Infrastructure.Base;

namespace VeiculosWeb.Infrastructure.Service.Paramedic
{
    public interface IParamedicChecklistReviewService : IBaseService<ParamedicChecklistReviewDTO>
    {
        Task<ResponseDTO> GetList(int? takeLast);
    }
}