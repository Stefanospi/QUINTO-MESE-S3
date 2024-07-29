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
        public Product CreateProduct(Product product)
        {
            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts()
        {
            return _dataContext.Products.ToList();
        }
    }
}
