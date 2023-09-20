using Core.Interfaces;
using Core.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProdcutType).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            return await _context.Products.Include(p => p.ProdcutType).Include(p => p.ProductBrand).ToListAsync(); 
        }

        public async Task<IReadOnlyList<ProdcutType>> GetProductTypes()
        {
            return await _context.ProdcutTypes.ToListAsync();
        }
    }
}