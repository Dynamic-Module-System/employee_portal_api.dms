using Application.Contrats;
using Application.DTOs;
using AutoMapper;
using Infrastructure.Contrats;
using System.Data;

namespace Application.Services
{
    public class BussineAppService : BaseAppService, IBussineAppService
    {
        private readonly IBussineRepository _bussineRepository;
        private readonly IMapper _mapper;
        public BussineAppService(IBussineRepository bussineRepository, IMapper mapper)
        {
            _bussineRepository = bussineRepository;
            _mapper = mapper;
        }

        public RequestResult<List<BussineDTO>> Get(string tenant)
        {
            try
            {
                var response = _bussineRepository.Get(tenant);
                List<BussineDTO> bussines = new();
                foreach (DataRow item in response.Rows)
                {
                    bussines.Add(_mapper.Map<DataRow, BussineDTO>(item));
                }

                return RequestResult<List<BussineDTO>>.CreateSuccessful(bussines);
            }
            catch (Exception ex)
            {
                return RequestResult<List<BussineDTO>>.CreateUnsuccessful(new string[] { ex.Message });
            }
        }
    }
}
