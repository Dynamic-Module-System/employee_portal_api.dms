using Infrastructure.Contrats;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IStoreProcedureCallRepository _storeProceduceCallRepository;
        
        public AuthRepository(IStoreProcedureCallRepository storeProceduceCallRepository)
        {
            _storeProceduceCallRepository = storeProceduceCallRepository;
        }

        public DataTable LogIn(List<(string, object)> parameters)
        {

            try
            {
                SqlParameter[] sqlParameters = _storeProceduceCallRepository.SetParams(parameters);
                DataSet response = _storeProceduceCallRepository.ExcecuteStoreProcedureSingle("GetUsuariosxNit_Web", sqlParameters);

                if (response.Tables.Count != 1) throw new ApplicationException("El SP no se ha configurado debidamente.");

                return response.Tables[0];
            }
            catch
            {
                throw;
            }

        }
    }
}
