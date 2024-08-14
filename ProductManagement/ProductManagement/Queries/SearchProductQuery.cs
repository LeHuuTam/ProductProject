using MediatR;
using ProductManagement.Models;

namespace ProductManagement.Queries
{
    public class SearchProductQuery : IRequest<List<ProductModel>>
    {
        public string Text { get; set; }
    }
}
