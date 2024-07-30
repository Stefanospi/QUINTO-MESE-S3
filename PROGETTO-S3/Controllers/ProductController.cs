using Microsoft.AspNetCore.Mvc;
using PROGETTO_S3.Services.Products;
using PROGETTO_S3.Models;
using System.Threading.Tasks;

namespace PROGETTO_S3.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("CreateProduct")]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost("CreateProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet("ProductList")]
        public async Task<IActionResult> ProductListAsync()
        {
            var products = await _productService.GetAllProducts();
            return PartialView("ProductListPartial", products);
        }
    }
}
