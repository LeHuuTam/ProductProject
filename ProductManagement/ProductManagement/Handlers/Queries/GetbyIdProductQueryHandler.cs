using MediatR;
using ProductManagement.Models;
using ProductManagement.Queries;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Queries
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductModel>
    {
        private readonly IProductService _productService;

        public GetByIdProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductModel> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetById(request.Id);

            return product;
        }
    }
}
