using Hangfire;
using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VeiculosWeb.Service
{
    public class AccountService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IUserRepository userRepository,
                                ITokenService tokenService,
                                IEmailService emailService,
                                IHttpContextAccessor httpContextAccessor) : IAccountService
    {
        private ISession Session => httpContextAccessor.HttpContext!.Session;

        private async Task<SignInResult> CheckUserPassword(User user, UserLoginDTO userLoginDTO)
        {
            try
            {
                return await signInManager.CheckPasswordSignInAsync(user, userLoginDTO.Password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar senha do usuário. Erro: {ex.Message}");
            }
        }

        private async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                return await userRepository.GetEntities().FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o usuário. Erro: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> Login(UserLoginDTO userDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var user = await GetUserByEmail(userDTO.Email);

                if (user == null)
                {
                    responseDTO.SetUnauthorized("Não autenticado! Verifique o email e a senha inserida!");
                    return responseDTO;
                }

                if (!user.EmailConfirmed)
                {
                    responseDTO.SetBadInput("Não autenticado! O email não foi confirmado!");
                    return responseDTO;
                }

                var password = await CheckUserPassword(user, userDTO);
                if (!password.Succeeded)
                {
                    responseDTO.SetUnauthorized("Não autenticado! Verifique o email e a senha inserida!");
                    return responseDTO;
                }

                responseDTO.Object = new
                {
                    userName = user.UserName,
                    role = user.Role.ToString(),
                    name = user.Name,
                    email = user.Email,
                    token = await tokenService.CreateToken(user)
                };
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetCurrent()
        {
            ResponseDTO responseDTO = new();
            try
            {
                var email = Session.GetString(Consts.ClaimEmail)!;
                Log.Information("Obtendo o usuário atual: {email}", email);
                responseDTO.Object = await GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> CreateUser(UserDTO userDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Role do usuário do DTO: {role}", userDTO.Role);
                if (userDTO.Role == RoleName.Admin)
                {
                    var requestUser = await userManager.FindByIdAsync(Session.GetString(Consts.ClaimUserId)!);
                    var requestUserRoleAdmin = requestUser!.Role == RoleName.Admin;
                    if (!requestUserRoleAdmin)
                    {
                        responseDTO.SetForbidden($"O usuário {requestUser!.Id} não pertence ao role {RoleName.Admin}");
                        return responseDTO;
                    }
                }

                var user = await userManager.FindByEmailAsync(userDTO.Email);
                if (user != null)
                {
                    responseDTO.SetBadInput($"Já existe um usuário cadastrado com este email: {userDTO.Email}!");
                    return responseDTO;
                }

                var userEntity = new User
                {
                    Name = userDTO.Name,
                    Role = userDTO.Role,
                    Email = userDTO.Email,
                    NormalizedEmail = userDTO.Email.ToUpper(),
                    NormalizedUserName = userDTO.Email.ToUpper()
                };

                userEntity.PasswordHash = userManager.PasswordHasher.HashPassword(userEntity, Guid.NewGuid().ToString());

                await userRepository.InsertAsync(userEntity);
                await userRepository.SaveChangesAsync();
                await userManager.UpdateSecurityStampAsync(userEntity);

                Log.Information("Usuário persistido id: {id}", userEntity.Id);

                await userRepository.SaveChangesAsync();

                Log.Information("Usuário adicionado no role: {role}", userDTO.Role);
                BackgroundJob.Enqueue(() => emailService.SendEmail("Solicitação para confirmar email - It's Check", emailService.BuildConfirmEmailText(userEntity.Email, userEntity.SecurityStamp!), userEntity.Email));
                responseDTO.Object = userEntity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateUser(Guid id, UserDTO userDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Role do usuário do DTO: {role}", userDTO.Role);
                var requestUser = await userRepository.GetEntities().FirstOrDefaultAsync(x=>x.Id == Session.GetString(Consts.ClaimUserId)!);

                if(requestUser!.Id != id.ToString() && requestUser.Role != RoleName.Admin)
                {
                    responseDTO.SetForbidden($"Não é possível editar outro usuário");
                    return responseDTO;
                }

                if (userDTO.Role == RoleName.Admin)
                {
                    var requestUserRoleAdmin = requestUser!.Role == RoleName.Admin;
                    if (!requestUserRoleAdmin)
                    {
                        responseDTO.SetForbidden($"O usuário {requestUser!.Id} não pertence ao role {RoleName.Admin}");
                        return responseDTO;
                    }
                }

                var userEntity = await userRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id.ToString());
                if (userEntity == null)
                {
                    responseDTO.SetBadInput($"Usuário não encotrado com este id: {id}!");
                    return responseDTO;
                }

                userEntity.Name = userDTO.Name;

                await userRepository.SaveChangesAsync();

                Log.Information("Usuário persistido id: {id}", userEntity.Id);

                var userRoles = await userManager.GetRolesAsync(userEntity);
                await userManager.RemoveFromRolesAsync(userEntity, userRoles);
                await userRepository.SaveChangesAsync();
                Log.Information("Usuário adicionado no role: {role}", userDTO.Role);

                await userRepository.SaveChangesAsync();

                responseDTO.Object = userEntity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> RemoveUser(Guid id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var requestUser = await userRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == Session.GetString(Consts.ClaimUserId)!);
                if (requestUser!.Id != id.ToString() && requestUser.Role != RoleName.Admin)
                {
                    responseDTO.SetForbidden($"Não é possível editar outro usuário");
                    return responseDTO;
                }

                var userEntity = await userRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id.ToString());
                if (userEntity == null)
                {
                    responseDTO.SetBadInput($"Usuário não encontrado com este id: {id}!");
                    return responseDTO;
                }

                var userRoles = await userManager.GetRolesAsync(userEntity);
                await userManager.RemoveFromRolesAsync(userEntity, userRoles);
                await userManager.DeleteAsync(userEntity);

                Log.Information("Usuário removido id: {id}", userEntity.Id);

                responseDTO.Object = userEntity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetUsers()
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await userRepository.GetEntities()
                                                          .Select(x => new
                                                          {
                                                              x.Id,
                                                              x.Name,
                                                              x.Email,
                                                              x.UserName,
                                                              x.Role
                                                          }).ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> RequestResetPassword(string email)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var user = await GetUserByEmail(email);
                if (user == null)
                {
                    responseDTO.SetBadInput($"Usuário não encontrado com o email: {email}");
                    return responseDTO;
                }

                BackgroundJob.Enqueue(() => emailService.SendEmail("Solicitação para redefinir senha - It's Check", emailService.BuildResetPasswordText(email, user.SecurityStamp!), email));
                responseDTO.Message = "Em breve, confira seu email!";
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> ResetPassword(UserEmailDTO userEmailDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var user = await GetUserByEmail(userEmailDTO.Email);
                if (user == null)
                {
                    responseDTO.SetBadInput($"Usuário não encontrado com o email: {userEmailDTO.Email}");
                    return responseDTO;
                }

                if (user.SecurityStamp != userEmailDTO.Code)
                {
                    responseDTO.SetBadInput($"O código {userEmailDTO.Code} é inválido!");
                    return responseDTO;
                }

                userRepository.Attach(user);
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, userEmailDTO.Password);
                await userRepository.SaveChangesAsync();
                await userManager.UpdateSecurityStampAsync(user);

                responseDTO.Message = "Email confirmado!";
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> ConfirmEmail(UserEmailDTO userEmailDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var user = await GetUserByEmail(userEmailDTO.Email);
                if (user == null)
                {
                    responseDTO.SetBadInput($"Usuário não encontrado com o email: {userEmailDTO.Email}");
                    return responseDTO;
                }

                if (user.EmailConfirmed)
                {
                    responseDTO.SetBadInput($"O email {userEmailDTO.Email} já foi confirmado!");
                    return responseDTO;
                }

                if (user.SecurityStamp != userEmailDTO.Code)
                {
                    responseDTO.SetBadInput($"O código {userEmailDTO.Code} é inválido!");
                    return responseDTO;
                }

                userRepository.Attach(user);
                user.EmailConfirmed = true;
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, userEmailDTO.Password);
                await userRepository.SaveChangesAsync();
                await userManager.UpdateSecurityStampAsync(user);

                responseDTO.Message = "Email confirmado!";
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
