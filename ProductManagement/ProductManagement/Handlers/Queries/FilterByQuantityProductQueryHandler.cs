using MediatR;
using ProductManagement.Models;
using ProductManagement.Queries;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Queries
{
    public class FilterByQuantityProductQueryHandler : IRequestHandler<FilterByQuantityProductQuery, List<ProductModel>>
    {
        private readonly IProductService _productService;

        public FilterByQuantityProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<ProductModel>> Handle(FilterByQuantityProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.FilterByQuantity(request.Quantity);

            return products;
        }
    }
}
