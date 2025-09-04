using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductByCode), new { code = createdProduct.Codigo }, createdProduct);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Product>> GetProductByCode(string code)
        {
            var product = await _productService.GetProductByCodeAsync(code);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var products = await _productService.GetProductsByCategoryAsync(category);
            return Ok(products);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateProduct(string code, Product product)
        {
            if (code != product.Codigo)
            {
                return BadRequest();
            }

            var updatedProduct = await _productService.UpdateProductAsync(product);
            if (updatedProduct == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{code}")]
        public async Task<IActionResult> PatchProduct(string code, [FromBody] Dictionary<string, object> updates)
        {
            await _productService.PatchProductAsync(code, updates);
            return NoContent();
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteProduct(string code)
        {
            var deleted = await _productService.DeleteProductAsync(code);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}