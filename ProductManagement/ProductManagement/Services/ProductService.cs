using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProductManagement.Commands;
using ProductManagement.Models;
using ProductManagement.Repositories;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string BlobConnStr = "DefaultEndpointsProtocol=https;AccountName=huutamstore;AccountKey=;EndpointSuffix=core.windows.net";


        public ProductService(IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductModel> CreateOrUpdate<T>(T command) where T : CreateOrUpdateProductCommand
        {
            var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            var product = new ProductModel
            {
                Name = command.Name,
                Price = command.Price,
                Description = command.Description,
                Quantity = command.Quantity,
                Image = null
            };
            if (command.Image != null && command.Image.Length != 0)
            {
                BlobContainerClient containerClient = new BlobContainerClient(BlobConnStr, "tam-container");
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

                // Tạo BlobClient
                string blobName = Guid.NewGuid() + Path.GetExtension(command.Image.FileName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                // Upload file lên Blob Storage
                using (var stream = command.Image.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = command.Image.ContentType });
                }

                // Trả về URL của file
                var imgUrl = blobClient.Uri.ToString();
                product.Image =  imgUrl;
            }
            
            //var product = _mapper.Map<ProductModel>(command);
            var isUPdate = command is IUpdateCommand;
            if (isUPdate)
            {
                product.Id = ((IUpdateCommand)command).Id;
                return await _productRepository.Update(product, userName);
            }

            return await _productRepository.Create(product, userName);
        }

        public async Task<ProductModel> Delete(int id)
        {
            var product = await _productRepository.Delete(id);
            return product;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return products;
        }
        public async Task<ProductModel> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            return product;
        }
        public async Task<List<ProductModel>> Search(string text)
        {
            var products = await _productRepository.Search(text);
            return products;
        }
        public async Task<List<ProductModel>> FilterByPrice(double minPrice, double maxPrice)
        {
            var products = await _productRepository.FilterByPrice(minPrice, maxPrice);
            return products;
        }
        public async Task<List<ProductModel>> FilterByQuantity(int quantity)
        {
            var products = await _productRepository.FilterByQuantity(quantity);
            return products;
        }
    }
}
