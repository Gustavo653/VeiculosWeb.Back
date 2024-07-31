using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.API.Controllers
{
    public class AccountController(IAccountService accountService) : BaseController
    {
        [HttpGet("Current")]
        public async Task<IActionResult> Current()
        {
            var user = await accountService.GetCurrent();
            return StatusCode(user.Code, user);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            var user = await accountService.Login(userLogin);
            return StatusCode(user.Code, user);
        }

        [HttpGet("")]
        [Authorize(Roles = $"{nameof(RoleName.Admin)}")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await accountService.GetUsers();
            return StatusCode(user.Code, user);
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var user = await accountService.CreateUser(userDTO);
            return StatusCode(user.Code, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserDTO userDTO)
        {
            var user = await accountService.UpdateUser(id, userDTO);
            return StatusCode(user.Code, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser([FromRoute] Guid id)
        {
            var user = await accountService.RemoveUser(id);
            return StatusCode(user.Code, user);
        }

        [HttpPost("RequestResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestResetPassword([FromBody] string email)
        {
            var user = await accountService.RequestResetPassword(email);
            return StatusCode(user.Code, user);
        }

        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] UserEmailDTO userEmailDTO)
        {
            var user = await accountService.ConfirmEmail(userEmailDTO);
            return StatusCode(user.Code, user);
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] UserEmailDTO userEmailDTO)
        {
            var user = await accountService.ResetPassword(userEmailDTO);
            return StatusCode(user.Code, user);
        }
    }
}