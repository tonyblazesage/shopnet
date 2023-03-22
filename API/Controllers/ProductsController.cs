using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.interfaces;
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
        
        private readonly IGenericRepo<Product> _productsrepo;
        private readonly IGenericRepo<ProductBrand> _productbrandrepo;
        private readonly IGenericRepo<ProductType> _producttyperepo;
        private readonly IMapper _mapper;
        
        public ProductsController(IGenericRepo<Product> productsrepo, IGenericRepo<ProductBrand> productbrandrepo, 
        IGenericRepo<ProductType> producttyperepo, IMapper mapper)
        {
            _mapper = mapper;
            _producttyperepo = producttyperepo;
            _productbrandrepo = productbrandrepo;
            _productsrepo = productsrepo;
                   
        }


        [HttpGet]
        public async Task <ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
         var spec = new ProductsWithTypesAndBrandsSpec();
           var products = await _productsrepo.ListAsync(spec);

           return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>> (products));
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);

            var product = await _productsrepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task <ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productbrandrepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _producttyperepo.ListAllAsync());
        }

    }
}