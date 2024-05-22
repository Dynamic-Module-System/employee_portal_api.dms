using Application.DTOs;

namespace Application.Contrats
{
    public interface IProofPaymentAppService
    {
        List<ProofPaymentDTO> Get(ProofPaymentDTO proofPaymentDTO);
    }
}
