using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAll();
        Task<ProductModel> GetById(int id);
        Task<List<ProductModel>> Search(string text);
        Task<List<ProductModel>> FilterByPrice(double minPrice, double maxPrice);
        Task<List<ProductModel>> FilterByQuantity(int quantity);
        Task<ProductModel> Create(ProductModel prd,string userName);
        Task<ProductModel> Update(ProductModel prd, string userName);
        Task<ProductModel> Delete(int id);
    }
}
