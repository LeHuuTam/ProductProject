using MediatR;
using ProductManagement.Models;
using ProductManagement.Queries;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IUserService _userService;

        public LoginQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var token = await _userService.Login(request.UserName, request.Password);
            return token;
        }
    }
}
