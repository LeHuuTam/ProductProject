using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductManagementDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ProductManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductModel> Create(ProductModel product, string userName)
        {
            var userId = _context.Users.First(u => u.UserName == userName).Id;
            var prd = _mapper.Map<Product>(product);
            prd.CreatedUserId = userId;
            prd.CreatedDate = DateTime.Now;
            prd.LastUpdatedUserId = userId;
            prd.LastUpdatedDate = DateTime.Now;
            await _context.Products.AddAsync(prd);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductModel>(prd);
        }

        public async Task<ProductModel> Delete(int id)
        {
            var prd = _context.Products.FirstOrDefault(p => p.Id == id);
            if (prd != null)
            {
                _context.Products.Remove(prd);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<ProductModel>(prd);
        }

        public async Task<List<ProductModel>> GetAll()
        {
            var prds = await _context.Products.OrderByDescending(p => p.CreatedDate).ToListAsync();
            return _mapper.Map<List<ProductModel>>(prds);
        }
        public async Task<ProductModel> GetById(int id)
        {
            var prd = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductModel>(prd);
        }
        public async Task<List<ProductModel>> Search(string text)
        {
            var prds = await _context.Products.Where(p => p.Name.Contains(text)).OrderByDescending(p => p.CreatedDate).ToListAsync();
            return _mapper.Map<List<ProductModel>>(prds);
        }
        public async Task<List<ProductModel>> FilterByPrice(double minPrice, double maxPrice)
        {
            var prds = await _context.Products.Where(p => p.Price>= minPrice && p.Price < maxPrice).OrderByDescending(p => p.CreatedDate).ToListAsync();
            return _mapper.Map<List<ProductModel>>(prds);
        }
        public async Task<List<ProductModel>> FilterByQuantity(int quantity)
        {
            var prds = await _context.Products.Where(p => p.Quantity >= quantity).OrderByDescending(p => p.CreatedDate).ToListAsync();
            return _mapper.Map<List<ProductModel>>(prds);
        }

        public async Task<ProductModel> Update(ProductModel product, string userName)
        {
            var userId = _context.Users.First(u => u.UserName == userName).Id;
            var prd = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (prd != null)
            {
                prd.Name = product.Name;
                prd.Price = product.Price;
                prd.Quantity = product.Quantity;
                prd.Description = product.Description;
                if(product.Image != null) { prd.Image = product.Image; }
                prd.LastUpdatedUserId = userId;
                prd.LastUpdatedDate = DateTime.Now;
                _context.Products.Update(prd);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<ProductModel>(prd);
        }
    }
}
