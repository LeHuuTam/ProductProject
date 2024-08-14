using MediatR;
using ProductManagement.Models;
using ProductManagement.Queries;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Queries
{
    public class FilterByPriceProductQueryHandler : IRequestHandler<FilterByPriceProductQuery, List<ProductModel>>
    {
        private readonly IProductService _productService;

        public FilterByPriceProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<ProductModel>> Handle(FilterByPriceProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.FilterByPrice(request.MinPrice, request.MaxPrice);

            return products;
        }
    }
}
