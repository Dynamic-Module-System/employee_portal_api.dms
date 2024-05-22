using Application.Contrats;
using Application.DTOs;
using AutoMapper;
using Infrastructure.Contrats;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Application.Services
{
    public class ProofPaymentAppService : BaseAppService, IProofPaymentAppService
    {
        public ProofPaymentAppService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMapper mapper) : base(httpContextAccessor, configuration, mapper) { }

        public List<ProofPaymentDTO> Get(ProofPaymentDTO dto)
        {
            //SqlParameter[] _params = _storeProcedureCallRepository.DefineParameters("id_contrato", dto.IdContrato, "mes", dto.Month, "ano", dto.Year);
            //var response = _storeProcedureCallRepository.ExcecuteStoreProcedureSingle("GetN_contrato_x_web", _params).Tables[0];

            List<ProofPaymentDTO> proofPayments = new();

            //foreach (DataRow item in response.Rows)
            //{
            //    proofPayments.Add(_mapper.Map<DataRow, ProofPaymentDTO>(item));
            //}
            return proofPayments;
        }
    }
}
