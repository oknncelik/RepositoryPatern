using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Business.Abstruct;
using Business.Concreate;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public HomeController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(new ServiceResult<ProductModel>(await _productManager.GetProduct(id)));
        }
        
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(ProductModel product)
        {
            return Ok(new ServiceResult<bool>(await _productManager.DeleteProduct(product)));
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(ProductFilter filter)
        {
            return Ok(new ServiceResult<IList<ProductModel>>(await _productManager.GetProducts(filter)));
        }
        
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ProductModel product)
        {
            return Ok(new ServiceResult<ProductModel>(await _productManager.AddProducts(product)));
        }
    }
}