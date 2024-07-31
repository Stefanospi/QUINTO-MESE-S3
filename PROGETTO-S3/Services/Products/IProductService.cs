using PROGETTO_S3.Models;
using PROGETTO_S3.Models.ViewModel;

namespace PROGETTO_S3.Services.Products
{
    public interface IProductService
    {
        public Task<Product> CreateProduct(CreateProductViewModel product);
        public Task<List<ingredients>> GetAllIngridients();
        public Task<List<Product>> GetAllProductIngridients();
        public Task<List<Product>> GetAllProducts();
    }
}
