using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrometheusDemo.Web.Models;
using System.Linq;
using Prometheus;

namespace PrometheusDemo.Web.Controllers
{
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly Random rnd = new Random();

        private static readonly Counter productsPurchasedMetric =
            Metrics.CreateCounter(
                "demoapp_productspurchased_total",
                "The total number of products purchased.",
                new string[] { "product_category" });

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

        [HttpGet("products")]
        public async Task<IActionResult> List()
        {
            await Task.Delay(rnd.Next(200, 400));

            return Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await Task.Delay(rnd.Next(50, 500));

            var product = products.FirstOrDefault(p => p.Id == id);

            return Ok(product);
        }

        [HttpPost("products/purchase/{id}")]
        public async Task<IActionResult> Purchase(int id)
        {
            await Task.Delay(rnd.Next(200, 500));

            var product = products.FirstOrDefault(p => p.Id == id);

            productsPurchasedMetric.WithLabels(product.Category).Inc();

            return Ok(product);
        }
    }
}
