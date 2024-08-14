using MediatR;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Commands
{
    public class DeleteProductCommand : IRequest<ProductModel>
    {
        public int Id { get; set; }

    }
}
