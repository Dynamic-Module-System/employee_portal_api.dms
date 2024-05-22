using Infrastructure.Contrats;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net.Http;

namespace Application.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IStoreProcedureCallRepository _storeProceduceCallRepository;

        public TenantMiddleware(RequestDelegate next, IConfiguration configuration, IStoreProcedureCallRepository storeProceduceCallRepository)
        {
            _next = next;
            _configuration = configuration;
            _storeProceduceCallRepository = storeProceduceCallRepository;
        }

        public async Task Invoke(HttpContext context)
        { 
            string tenant = context.Request.Headers["Tenant"];
            string nit = context.Request.Headers["Nit"];
            if (string.IsNullOrEmpty(tenant)) throw new ApplicationException("El Tenant es requerido.");

            context.Items["ConnectionString"] = _configuration.GetConnectionString("DefaultConnection");
            DataTable response = GetTenant(tenant, nit);
            if (response.Rows.Count == 0) throw new ApplicationException("El Tenant proporcionado no existe en la nuestro sistema.");

            if (response.Rows.Count > 1)
            {
                await _next(context);
                return;
            }

            context.Items["ConnectionString"] = response.Rows[0]["connectionString"];
            context.Items["Nit_Empresa"] = response.Rows[0]["nit"];
            await _next(context);
        }

        private DataTable GetTenant(string TenantID, string Nit)
        {
            string query = "select nit, connectionString = " +
                                   "CONCAT('Data Source=',ipservidor,'; Initial Catalog=',base_datos,'; User ID=',user_session,'; Password=',pass_session,'; Max Pool Size=9999')" +
                               "From nom_empresa where base_datos = '" + TenantID + "'";

            if (!string.IsNullOrEmpty(Nit)) query += $"and nit = {Nit}";

            return _storeProceduceCallRepository.ExecuteQuery(query).Tables[0];
        }
    }
}
