using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstruct;
using Business.Concreate;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly IProductManager _productManager;

        public HomeController()
        {
            _productManager = new ProductManager();
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(ProductFilter filter)
        {
            return Ok(await _productManager.GetProducts(filter));
        }
        
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ProductModel product)
        {
            return Ok(await _productManager.AddProducts(product));
        }
    }
}