using Infrastructure.Contrats;
using System.Data;

namespace Infrastructure.Services
{
    public class BussineRepository : IBussineRepository
    {
        private readonly IStoreProcedureCallRepository _storeProcedureCallRepository;

        public BussineRepository(IStoreProcedureCallRepository storeProcedureCallRepository)
        {
            _storeProcedureCallRepository = storeProcedureCallRepository;
        }

        public DataTable Get(string tenant)
        {
            try
            {
                string query = "select id, nombre, nit, connectionString = " +
                                   "CONCAT('Data Source=',ipservidor,'; Initial Catalog=',base_datos,'; User ID=',user_session,'; Password=',pass_session,'; Max Pool Size=9999')" +
                               "From nom_empresa where base_datos = '" + tenant + "'";

                return _storeProcedureCallRepository.ExecuteQuery(query).Tables[0];
            }
            catch
            {
                throw;
            }
        }
    }
}
