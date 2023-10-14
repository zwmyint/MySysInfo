using Microsoft.AspNetCore.Mvc;
using MyWebApi.Minimal.Entities;

namespace MyWebApi.Minimal.Controllers
{
    //
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Milo" },
            new Product { Id = 2, Name = "Tim Tams" },
            new Product { Id = 3, Name = "Milo 3" },
            new Product { Id = 4, Name = "Tim Tams 4" }
        };

        private static readonly string[] Compliments = new[]
        {
          "You're a shining star!",
          "Your smile is contagious.",
          "You're an inspiration to others.",
          "You have a heart of gold.",
          "You light up the room.",
          "You're a superhero without a cape.",
          "You bring joy wherever you go."
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.Find(x => x.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("GetRandomC")]
        public ActionResult<string> GetRandomCompliment()
        {
            Random random = new Random();
            int index = random.Next(Compliments.Length);
            return Ok(Compliments[index]);
        }
    }
    //
}

