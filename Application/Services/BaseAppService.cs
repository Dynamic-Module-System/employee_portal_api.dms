using AutoMapper;
using Infrastructure.Contrats;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class BaseAppService
    {
        public readonly HttpContext httpContext;
        public readonly IConfiguration _configuration;
        public IMapper _mapper { get; }

        public BaseAppService()
        {

        }

        public BaseAppService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMapper mapper)
        {
            httpContext = httpContextAccessor.HttpContext;
            _configuration = configuration;
            _mapper = mapper;
        }
    }
}
