using MediatR;
using ProductManagement.Models;
using ProductManagement.Queries;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Queries
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductModel>>
    {
        private readonly IProductService _productService;

        public GetAllProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<ProductModel>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetAll();

            return products;
        }
    }
}
