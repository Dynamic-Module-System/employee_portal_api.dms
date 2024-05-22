using Application.Contrats;
using Application.DTOs;
using Application.Middlewares;
using AutoMapper;
using Infrastructure.Contrats;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Application.Services
{
    public class AuthAppService : BaseAppService, IAuthAppService
    {
        private readonly IAuthRepository _authRepository;

        public AuthAppService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMapper mapper, IAuthRepository authRepository) : base(httpContextAccessor, configuration, mapper) 
        {
            _authRepository = authRepository;
        }

        public RequestResult<UserDTO> LogIn(UserDTO dto)
        {
            try
            {
                List<(string, object)> parameters = new();
                parameters.Add(("nit", dto.Username));
                var response = _authRepository.LogIn(parameters);
                if (response.Rows.Count == 0) throw new ApplicationException("Credenciales incorrectas");
                var securityKey32 = _configuration["SecurityKey32"].ToString();
                var securityKey16 = _configuration["SecurityKey16"].ToString();
                var passwordDecrypt = AesEncryption.Decrypt(dto.Password, securityKey32, securityKey16);
                if (response.Rows[0]["clave"].ToString() != passwordDecrypt) throw new ApplicationException("Credenciales incorrectas");
                UserDTO user = _mapper.Map<DataRow, UserDTO>(response.Rows[0]);
                user.Token = $"Bearer {GenerarToken(response.Rows[0])}";
                return RequestResult<UserDTO>.CreateSuccessful(user);
            }
            catch(Exception ex) 
            {
                return RequestResult<UserDTO>.CreateUnsuccessful(new string[] { ex.Message } );
            }
        }

        private string GenerarToken(DataRow data)
        {
            var claveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey32"]));
            var credenciales = new SigningCredentials(claveSecreta, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Status", data["estado"].ToString()),
                new Claim(("Id"), data["id"].ToString()),
                new Claim(("Nit"), data["nit"].ToString()),
                new Claim(("Email"), data["email"].ToString()),
                new Claim(("Created_at"), data["fecha_creacion"].ToString()),
                new Claim(("Name"), data["nombre"].ToString()),
                new Claim(("IdEmpresa"), data["id_empresa"].ToString()),
                new Claim(("IdContrato"), data["id_contrato"].ToString()),
                new Claim(("IdBoss"), data["id_jefe"].ToString()),
                new Claim(("DataTratament"), data["tratamiento_datos"].ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7167/swagger/index.html",
                audience: data["nit"].ToString(),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
