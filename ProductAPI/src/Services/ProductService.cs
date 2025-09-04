using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByCodeAsync(string codigo)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string categoria)
        {
            return await _context.Products.Where(p => p.Categoria == categoria).ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(string codigo)
        {
            var product = await GetProductByCodeAsync(codigo);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        internal async Task PatchProductAsync(string code, Dictionary<string, object> updates)
        {
            var product = await GetProductByCodeAsync(code) ?? throw new KeyNotFoundException("Product not found");
            foreach (var update in updates)
            {
                var propertyInfo = typeof(Product).GetProperty(update.Key);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(product, Convert.ChangeType(update.Value, propertyInfo.PropertyType));
                }
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}