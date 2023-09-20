using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = String.Empty;
        public int ProductTypeId { get; set; }
        public ProdcutType? ProdcutType { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; }
    }
}