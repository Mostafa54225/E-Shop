
using API.Dtos.ProductDtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Product;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<Pagination<ProductToRerturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            var totalItems = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductToRerturnDto>>(products);
            return Ok(new p<ProductToRerturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToRerturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if(product == null) return NotFound(new ApiResponse(404, "Product Not Found"));
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