using Microsoft.AspNetCore.Mvc;
using PROGETTO_S3.Services.Products;
using PROGETTO_S3.Models;
using System.Threading.Tasks;
using PROGETTO_S3.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace PROGETTO_S3.Controllers
{

    [Route("Products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            var ingredients = await _productService.GetAllIngridients();
            var viewModel = new CreateProductViewModel
            {
                Product = new Product
                {
                    Name = string.Empty,
                    Price = 0.0m,
                    DeliveryTimeInMinutes = 0
                },
                Ingredients = ingredients
            };
            return View(viewModel);
        }
        [HttpPost("CreateProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.CreateProduct(viewModel);
                return RedirectToAction("ProductList");
            }
            return View(viewModel);
        }


        [HttpGet("ProductList")]
        public async Task<IActionResult> ProductList()
        {
            var products = await _productService.GetAllProducts();
            return PartialView("ProductList", products);
        }
    }
}
