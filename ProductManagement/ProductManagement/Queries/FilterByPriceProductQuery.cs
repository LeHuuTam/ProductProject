using MediatR;
using ProductManagement.Models;

namespace ProductManagement.Queries
{
    public class FilterByPriceProductQuery : IRequest<List<ProductModel>>
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}
