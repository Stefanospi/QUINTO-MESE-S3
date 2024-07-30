using Microsoft.EntityFrameworkCore;
using PROGETTO_S3.Models;

namespace PROGETTO_S3.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dataContext.Products.ToListAsync();
        }
    }
}
