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
        
        public ProductsController(IGenericRepo<Product> productsrepo, IGenericRepo<ProductBrand> productbrandrepo, IGenericRepo<ProductType> producttyperepo)
        {
            _producttyperepo = producttyperepo;
            _productbrandrepo = productbrandrepo;
            _productsrepo = productsrepo;
                   
        }


        [HttpGet]
        public async Task <ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpec();
           var products = await _productsrepo.ListAsync(spec);
           return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);

            return await _productsrepo.GetEntityWithSpec(spec);
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