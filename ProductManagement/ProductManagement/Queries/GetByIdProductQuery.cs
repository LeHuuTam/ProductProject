using MediatR;
using ProductManagement.Models;

namespace ProductManagement.Queries
{
    public class GetByIdProductQuery : IRequest<ProductModel>
    {
        public int Id { get; set; }
    }
}
