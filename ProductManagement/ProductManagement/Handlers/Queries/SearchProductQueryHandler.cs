using MediatR;
using ProductManagement.Models;
using ProductManagement.Queries;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Queries
{
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, List<ProductModel>>
    {
        private readonly IProductService _productService;

        public SearchProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<ProductModel>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.Search(request.Text);

            return products;
        }
    }
}
