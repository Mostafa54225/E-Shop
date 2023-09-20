
using API.Dtos.ProductDtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Product;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToRerturnDto>>> GetProducts()
        {
            var products = await _repo.GetProducts();
            var p = _mapper.Map<IReadOnlyCollection<ProductToRerturnDto>>(products);
            return Ok(p);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductById(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrands());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProdcutType>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypes());
        }
    }
}