using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IUserService
    {
        Task<UserModel> GetByUserName(string userName);
        Task<string> Login(string userName, string pwd);
        Task<bool> Authenticate(string userName, string pwd);
    }
}
