using Application.DTOs;

namespace Application.Contrats
{
    public interface IBussineAppService
    {
        RequestResult<List<BussineDTO>> Get(string tenant);
    }
}
