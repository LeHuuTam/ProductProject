using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductManagementDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ProductManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserModel> GetByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync<User>(x => x.UserName == userName);
            return _mapper.Map<UserModel>(user);
        }
    }
}
