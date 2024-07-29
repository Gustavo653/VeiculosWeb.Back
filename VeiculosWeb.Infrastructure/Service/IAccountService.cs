using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IAccountService
    {
        Task<ResponseDTO> RequestResetPassword(string email);
        Task<ResponseDTO> ResetPassword(UserEmailDTO userEmailDTO);
        Task<ResponseDTO> ConfirmEmail(UserEmailDTO userEmailDTO);
        Task<ResponseDTO> CreateUser(UserDTO userDTO);
        Task<ResponseDTO> UpdateUser(Guid id, UserDTO userDTO);
        Task<ResponseDTO> RemoveUser(Guid id);
        Task<ResponseDTO> GetUsers();
        Task<ResponseDTO> GetCurrent();
        Task<ResponseDTO> Login(UserLoginDTO userLoginDTO);
    }
}