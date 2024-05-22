using Application.Contrats;
using Application.IMapperProfile;
using Application.Middlewares;
using Application.Services;
using AutoMapper;
using Infrastructure.Contrats;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar IHttpContextAccessor en el contenedor de servicios
builder.Services.AddHttpContextAccessor();

// Configurar autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            //ValidIssuer = "tu_issuer",
            //ValidAudience = "tu_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey32"].ToString()))
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Add Injection dependency */
builder.Services.AddSingleton<IAuthAppService, AuthAppService>();
builder.Services.AddSingleton<IProofPaymentAppService, ProofPaymentAppService>();
builder.Services.AddSingleton<IBussineAppService, BussineAppService>();
builder.Services.AddSingleton<IBussineRepository, BussineRepository>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IStoreProcedureCallRepository, StoreProceduceCallRepository>();

/* Add IMapper */
var mapperConfig = new MapperConfiguration(mapperConfig => mapperConfig.AddProfile(new MappingProfiles()));
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<TenantMiddleware>();
app.Run();
