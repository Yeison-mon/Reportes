using MIS_REPORTES.Models;

namespace MIS_REPORTES.Service
{
    public interface IUserService
    {
        Task<RbacUsuarios> Authenticate(string username, string password);
        Task<bool> Register(string username, string password, string email);
        Task<RbacUsuarios> GetUserById(int userId);
    }
}
