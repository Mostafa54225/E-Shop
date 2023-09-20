using Core.Models.Product;

namespace Core.Interfaces
{
    public interface IProductRepo
    {
        Task<Product> GetProductById(int id);
        Task<IReadOnlyList<Product>> GetProducts();
        Task<IReadOnlyList<ProdcutType>> GetProductTypes();
        Task<IReadOnlyList<ProductBrand>> GetProductBrands();
    }
}