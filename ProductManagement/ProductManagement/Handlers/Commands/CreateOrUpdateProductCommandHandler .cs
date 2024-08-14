using AutoMapper;
using MediatR;
using ProductManagement.Commands;
using ProductManagement.Models;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Commands
{
    public class CreateOrUpdateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductModel>,
        IRequestHandler<UpdateProductCommand, ProductModel>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CreateOrUpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateOrUpdate(request);
            return product;
        }

        public async Task<ProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateOrUpdate(request);
            return product;
        }
    }
}
