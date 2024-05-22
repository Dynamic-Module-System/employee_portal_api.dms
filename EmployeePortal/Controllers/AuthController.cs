using Application.Contrats;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;
        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        [HttpPost("Authentication")]
        [AllowAnonymous]
        public RequestResult<UserDTO> LogIn([FromBody] UserDTO user)
        {
            return _authAppService.LogIn(user);
        }
    }
}
