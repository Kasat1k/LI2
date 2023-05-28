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
    public class CategoryOfProductsController : ControllerBase
    {
        private readonly LI2Context _context;

        public CategoryOfProductsController(LI2Context context)
        {
            _context = context;
        }

        // GET: api/CategoryOfProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryOfProduct>>> GetCategoryOfProducts()
        {
          if (_context.CategoryOfProducts == null)
          {
              return NotFound();
          }
            return await _context.CategoryOfProducts.ToListAsync();
        }

        // GET: api/CategoryOfProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryOfProduct>> GetCategoryOfProduct(int id)
        {
          if (_context.CategoryOfProducts == null)
          {
              return NotFound();
          }
            var categoryOfProduct = await _context.CategoryOfProducts.FindAsync(id);

            if (categoryOfProduct == null)
            {
                return NotFound();
            }

            return categoryOfProduct;
        }

        // PUT: api/CategoryOfProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryOfProduct(int? id, CategoryOfProductDTO categoryOfProductDTO)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var categoryOfProduct = await _context.CategoryOfProducts.FindAsync(id);
            if (categoryOfProduct is null) { return BadRequest(); }
            categoryOfProduct.Name = categoryOfProductDTO.Name;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryOfProductExists(categoryOfProduct.Id))
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

        // POST: api/CategoryOfProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryOfProduct>> PostCategoryOfProduct(CategoryOfProductDTO categoryOfProductDTO)
        {
          if (_context.CategoryOfProducts == null)
          {
              return Problem("Entity set 'LI2Context.CategoryOfProducts'  is null.");
          }
            var categoryOfProduct = new CategoryOfProduct()
            {
                Id = categoryOfProductDTO.Id,
                Name = categoryOfProductDTO.Name,
            };
            _context.CategoryOfProducts.Add(categoryOfProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryOfProduct", new { id = categoryOfProduct.Id }, categoryOfProduct);
        }

        // DELETE: api/CategoryOfProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryOfProduct(int id)
        {
            if (_context.CategoryOfProducts == null)
            {
                return NotFound();
            }
            var categoryOfProduct = await _context.CategoryOfProducts.FindAsync(id);
            if (categoryOfProduct == null)
            {
                return NotFound();
            }

            _context.CategoryOfProducts.Remove(categoryOfProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryOfProductExists(int id)
        {
            return (_context.CategoryOfProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
