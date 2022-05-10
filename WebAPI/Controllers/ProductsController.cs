using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely Coupled=Gevşek Bağımlılık/Sadece soyuta bağımlılık var.
        //Nameing Convention. private için=>_productService

        IProductService _productService; //field

        //IoC Container__Inversion Of Control
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll() //Datadan veri çekerken
        {
            //Depency Chain=Bağımlılık Zinciri
            //IProductService productService = new ProductManager(new EfProductDal());

            var result= _productService.GetAll();
            if (result.Success)
            {
                return Ok(result); //200
            }
            return BadRequest(result);//400 Hatalıysa
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product) //Dataya veri eklerken
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
