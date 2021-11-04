using CoreWebApiEfCrud.DataAccess;
using CoreWebApiEfCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoreWebApiEfCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private NorthwindContext _context;

        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var productList = await _context.Products.ToListAsync();

            return Ok(productList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            _context.Products.Add(product);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return CreatedAtAction("PostProduct", new { id = product.ProductId }, product);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}