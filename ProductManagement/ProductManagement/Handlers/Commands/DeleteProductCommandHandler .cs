using AutoMapper;
using MediatR;
using ProductManagement.Commands;
using ProductManagement.Models;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductModel>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            

            var product = await _productService.Delete(request.Id);
            return product;
        }   
    }
}
