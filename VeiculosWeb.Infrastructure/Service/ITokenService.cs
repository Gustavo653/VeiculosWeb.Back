using VeiculosWeb.Domain.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface ITokenService
    {
        Task<string> CreateToken(User userDTO);
    }
}