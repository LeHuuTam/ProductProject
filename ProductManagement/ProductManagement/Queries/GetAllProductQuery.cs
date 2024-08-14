using MediatR;
using ProductManagement.Models;

namespace ProductManagement.Queries
{
    public class GetAllProductQuery : IRequest<List<ProductModel>>
    {
    }
}
