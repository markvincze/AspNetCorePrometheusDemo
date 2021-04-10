using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrometheusDemo.Web.Models;
using System.Linq;

namespace PrometheusDemo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Category = "Hardware Tools",
                Name = "Hammer",
                Price = 22.0m
            },
            new Product
            {
                Id = 2,
                Category = "Toys",
                Name = "LEGO",
                Price = 53m
            },
            new Product
            {
                Id = 3,
                Category = "Fashion",
                Name = "Sunglasses",
                Price = 75.0m
            },
        };

        public ProductsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var rnd = new Random();

            await Task.Delay(rnd.Next(100, 400));

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rnd = new Random();

            await Task.Delay(rnd.Next(50, 200));

            var product = products.FirstOrDefault(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
