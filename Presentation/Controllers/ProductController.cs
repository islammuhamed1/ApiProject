using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.ProductDtos;
namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class ProductController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>>GetAllProducts()
        {
            var products = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(products);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetProduct(int id)
        {
            var products = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(products);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
