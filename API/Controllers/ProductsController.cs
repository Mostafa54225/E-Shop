
using API.Dtos.ProductDtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Product;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepo<Product> _productRepo;
        private readonly IGenericRepo<ProdcutType> _productTypeRepo;
        private readonly IGenericRepo<ProductBrand> _producBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepo<Product> productRepo,
            IGenericRepo<ProdcutType> productTypeRepo,
            IGenericRepo<ProductBrand> producBrandRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _producBrandRepo = producBrandRepo;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToRerturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            var prodtuctDto = _mapper.Map<IReadOnlyCollection<ProductToRerturnDto>>(products);
            return Ok(prodtuctDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToRerturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            var productDto = _mapper.Map<ProductToRerturnDto>(product);
            return productDto;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _producBrandRepo.ListAll());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProdcutType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAll());
        }
    }
}