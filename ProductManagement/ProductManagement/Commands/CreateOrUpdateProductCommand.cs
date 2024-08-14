using MediatR;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Commands
{
    public interface ICreateCommand
    {

    }

    public interface IUpdateCommand
    {
        int Id { get; set; }
    }
    public abstract class CreateOrUpdateProductCommand : IRequest<ProductModel>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateProductCommand : CreateOrUpdateProductCommand, IUpdateCommand
    {
        public int Id { get; set; }

    }
    public class CreateProductCommand : CreateOrUpdateProductCommand, ICreateCommand
    {

    }
}
