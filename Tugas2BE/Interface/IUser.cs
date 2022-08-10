using Tugas2BE.DTO;

namespace Tugas2BE.Interface
{
    public interface IUser
    {
        Task Registration(CreateUserDTO user);
        Task<UserDTO> Authenticate(string username, string password);
        Task<IEnumerable<UserDTO>> GetAll();
    }
}
