using background_job_real_world_demo.Models;
using background_job_real_world_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace background_job_real_world_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCache _productCache;
        private readonly IProductService _productService;

        public ProductsController(IProductCache productCache, IProductService productService)
        {
            _productCache = productCache ?? throw new ArgumentNullException(nameof(productCache));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet("cache")]
        public IEnumerable<Product> GetProductsCache()
        {
            return _productCache.GetProducts();
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetProducts();
        }
    }
}