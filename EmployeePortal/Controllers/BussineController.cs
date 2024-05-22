using Application.Contrats;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BussineController : ControllerBase
    {
        private readonly IBussineAppService _bussineAppService;

        public BussineController(IBussineAppService bussineAppService)
        {
            _bussineAppService = bussineAppService; 
        }

        [HttpGet]
        [AllowAnonymous]
        public RequestResult<List<BussineDTO>> Get([FromHeader] string tenant)
        {
            return _bussineAppService.Get(tenant);
        }
    }
}
