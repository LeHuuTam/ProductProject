using MediatR;
using ProductManagement.Models;

namespace ProductManagement.Queries
{
    public class FilterByQuantityProductQuery : IRequest<List<ProductModel>>
    {
        public int Quantity { get; set; }
    }
}
