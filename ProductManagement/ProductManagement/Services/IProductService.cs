using ProductManagement.Commands;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAll();
        Task<ProductModel> GetById(int id);
        Task<List<ProductModel>> Search(string text);
        Task<List<ProductModel>> FilterByPrice(double minPrice, double maxPrice);
        Task<List<ProductModel>> FilterByQuantity(int quantity);
        Task<ProductModel> CreateOrUpdate<T>(T command)
            where T : CreateOrUpdateProductCommand;
        Task<ProductModel> Delete(int id);
    }
}
