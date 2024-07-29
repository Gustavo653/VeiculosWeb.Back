using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeiculosWeb.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
