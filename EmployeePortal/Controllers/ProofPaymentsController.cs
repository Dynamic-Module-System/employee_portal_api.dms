using Application.Contrats;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProofPaymentsController : ControllerBase
    {
        private readonly IProofPaymentAppService _proofPaymentsAppService;

        public ProofPaymentsController(IProofPaymentAppService proofPaymentAppService)
        {
            _proofPaymentsAppService = proofPaymentAppService;
        }

        [HttpPost("Get")]
        [Authorize]
        public List<ProofPaymentDTO> Get([FromBody] ProofPaymentDTO proofPayment)
        {
            return _proofPaymentsAppService.Get(proofPayment);
        }
    }
}
