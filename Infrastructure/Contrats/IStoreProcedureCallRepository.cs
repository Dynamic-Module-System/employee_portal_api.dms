using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Contrats
{
    public interface IStoreProcedureCallRepository
    {
        SqlParameter[] SetParams(List<(string Key, object Value)> parameters);
        DataSet ExcecuteStoreProcedureSingle(string name, SqlParameter[] parameters);
        DataSet ExecuteQuery(String query);
    }
}
