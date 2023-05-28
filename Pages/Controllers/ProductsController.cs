using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LI2.Pages.Models;
using LI2.Pages.ModelsDTO;
namespace LI2.Pages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly LI2Context _context;

        public ProductsController(LI2Context context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int? id, ProductDTO productDTO)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);
            if(product is null) { return BadRequest(); }
            var Orders  = await _context.OrderToProducts.Where(Order => Order.Id == product.OrderToProductId).ToListAsync();
            var Types = await _context.TypeOfProducts.Where(TypeOfProduct => TypeOfProduct.Id ==  product.TypeOfProductId).ToListAsync();
            var categories = await _context.CategoryOfProducts.Where(Category => Category.Id == product.CategoryOfProductId).ToListAsync();
           
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.CategoryOfProductId= productDTO.CategoryOfProductId;
            product.CategoryOfProduct = categories.FirstOrDefault();
            product.OrderToProductId = productDTO.OrderToProductId;
            product.OrderToProduct = Orders.FirstOrDefault();
            product.TypeOfProductId= productDTO.TypeOfProductId;
            product.TypeOfProduct = Types.FirstOrDefault();
            product.OrderId = 0;
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO productDTO)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'LI2Context.Products'  is null.");

            }
            var Orders = await _context.OrderToProducts.Where(Order => Order.Id == productDTO.OrderToProductId).ToListAsync();
            var Types = await _context.TypeOfProducts.Where(TypeOfProduct => TypeOfProduct.Id == productDTO.TypeOfProductId).ToListAsync();
            var categories = await _context.CategoryOfProducts.Where(Category => Category.Id == productDTO.CategoryOfProductId).ToListAsync();
            var product = new Product() {
                OrderId = 0,
                Name = productDTO.Name,
            Description = productDTO.Description,
            Price = productDTO.Price,
            CategoryOfProductId = productDTO.CategoryOfProductId,
            CategoryOfProduct = categories.FirstOrDefault(),
            OrderToProductId = productDTO.OrderToProductId,
            OrderToProduct = Orders.FirstOrDefault(),
            TypeOfProductId = productDTO.TypeOfProductId,
            TypeOfProduct = Types.FirstOrDefault()
        };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
