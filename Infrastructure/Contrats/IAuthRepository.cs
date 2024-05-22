using System.Data;

namespace Infrastructure.Contrats
{
    public interface IAuthRepository
    {
        DataTable LogIn(List<(string, object)> parameters);
    }
}
