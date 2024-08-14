using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetByUserName(string username);
    }
}
