using MediatR;
using ProductManagement.Models;

namespace ProductManagement.Queries
{
    public class LoginQuery : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
