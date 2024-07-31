using Microsoft.EntityFrameworkCore;
using PROGETTO_S3.Models;
using PROGETTO_S3.Models.ViewModel;


namespace PROGETTO_S3.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task<Product> CreateProduct(CreateProductViewModel viewModel)
        {
            var product = viewModel.Product;

            if (viewModel.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.Image.CopyToAsync(memoryStream);
                    product.Photo = memoryStream.ToArray();
                }
            }

            product.Ingredients = await _dataContext.Ingredients
                .Where(i => viewModel.SelectedIngredientIds.Contains(i.Id))
                .ToListAsync();

            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProductIngridients()
        {
            return await _dataContext.Products
                .Include(p => p.Ingredients)
                .ToListAsync();
        }

        public async Task<List<ingredients>> GetAllIngridients()
        {
            return await _dataContext.Ingredients.ToListAsync();

        }

        async Task<List<Product>> IProductService.GetAllProductIngridients()
        {
            return await _dataContext.Products
                .Include (p => p.Ingredients)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dataContext.Products.ToListAsync();
        }
    }
}
