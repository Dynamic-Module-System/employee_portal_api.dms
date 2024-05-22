using System.Data;

namespace Infrastructure.Contrats
{
    public interface IBussineRepository
    {
        DataTable Get(string tenant);
    }
}
