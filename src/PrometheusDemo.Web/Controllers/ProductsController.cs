using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrometheusDemo.Web.Models;
using System.Linq;
using System.Diagnostics;

namespace PrometheusDemo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly Random rnd = new Random();

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

        private readonly IMetricsStore metricsStore;

        public ProductsController(IMetricsStore metricsStore)
        {
            this.metricsStore = metricsStore;
        }

        private async Task<T> Measure<T>(string route, Func<Task<T>> runWithMeasure)
        {
            var sw = Stopwatch.StartNew();

            var result = await runWithMeasure();

            sw.Stop();

            metricsStore.ObserveResponseTimeDuration(route, sw.Elapsed.TotalSeconds);

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return await Measure("/products", async () =>
            {
                await Task.Delay(rnd.Next(10, 3000));

                return Ok(products);
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await Measure("/products/{id}", async () =>
            {
                await Task.Delay(rnd.Next(50, 150));

                var product = products.FirstOrDefault(p => p.Id == id);

                return Ok(product);
            });
        }

        [HttpPost("purchase/{id}")]
        public async Task<IActionResult> Purchase(int id)
        {
            await Task.Delay(rnd.Next(200, 500));

            var product = products.FirstOrDefault(p => p.Id == id);

            metricsStore.ObserveProductPurchased(product.Category);

            return Ok(product);
        }
    }
}
