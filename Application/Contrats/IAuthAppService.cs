using Application.DTOs;

namespace Application.Contrats
{
    public interface IAuthAppService
    {
        public RequestResult<UserDTO> LogIn(UserDTO dto);
    }
}
