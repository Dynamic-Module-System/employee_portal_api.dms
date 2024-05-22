using Infrastructure.Contrats;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Services
{
    public class StoreProceduceCallRepository : IStoreProcedureCallRepository
    {
        private SqlConnection _connection;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _adapter;
        private DataSet _dataSet = new();
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoreProceduceCallRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private void PrepareConnection(string exec)
        {
            _dataSet = new();
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            _connection = new SqlConnection(httpContext.Items["ConnectionString"].ToString());
            _adapter = new SqlDataAdapter();
            _connection.Open();
            _sqlCommand = new SqlCommand(exec, _connection);
        }

        private DataSet Exec()
        {
            _adapter.SelectCommand = _sqlCommand;
            _adapter.Fill(_dataSet);
            return _dataSet;
        }

        public DataSet ExcecuteStoreProcedureSingle(string name, SqlParameter[] parameters)
        {
            try
            {
                PrepareConnection(name);
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddRange(parameters);
                return Exec();
            }
            catch /*(Exception ex)*/
            {
                throw;
                //throw new ApplicationException($"Error al ejecutar el Stored Procedure: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }

        public DataSet ExecuteQuery(String query)
        {
            try
            {
                PrepareConnection(query);
                return Exec();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al ejecutar el Query: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }

        public SqlParameter[] SetParams(List<(string Key, object Value)> parameters)
        {
            List<SqlParameter> newParams = new();
            foreach (var param in parameters)
            {
                newParams.Add(new SqlParameter($"@{param.Key}", param.Value));
            }
            return newParams.ToArray();
        }
    }
}