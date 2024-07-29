using Microsoft.AspNetCore.Mvc;
using PROGETTO_S3.Services.Products;
using PROGETTO_S3.Models;

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
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
