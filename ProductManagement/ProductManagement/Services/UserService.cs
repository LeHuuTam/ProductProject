using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Data;
using ProductManagement.Models;
using ProductManagement.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<UserModel> GetByUserName(string userName)
        {
            var user = await _userRepository.GetByUserName(userName);
            return user;
        }
        public async Task<string> Login(string userName, string pwd)
        {
            string tokenResult = "";
            var user = await _userRepository.GetByUserName(userName);

            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserName", user.UserName)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenResult = tokenHandler.WriteToken(token);

            }
            return tokenResult;
        }
        public async Task<bool> Authenticate(string userName, string pwd)
        {
            var user = await _userRepository.GetByUserName(userName);
            if (user == null || !user.Password.Equals(pwd))
            {
                return false;
            }
            return true;
        }
    }
}
