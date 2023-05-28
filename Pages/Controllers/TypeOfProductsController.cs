using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LI2.Pages.Models;
using LI2.Pages.ModelsDTO;
using System.Runtime.InteropServices;

namespace LI2.Pages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfProductsController : ControllerBase
    {
        private readonly LI2Context _context;

        public TypeOfProductsController(LI2Context context)
        {
            _context = context;
        }

        // GET: api/TypeOfProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfProduct>>> GetTypeOfProducts()
        {
          if (_context.TypeOfProducts == null)
          {
              return NotFound();
          }
            return await _context.TypeOfProducts.ToListAsync();
        }

        // GET: api/TypeOfProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfProduct>> GetTypeOfProduct(int id)
        {
          if (_context.TypeOfProducts == null)
          {
              return NotFound();
          }
            var typeOfProduct = await _context.TypeOfProducts.FindAsync(id);

            if (typeOfProduct == null)
            {
                return NotFound();
            }

            return typeOfProduct;
        }

        // PUT: api/TypeOfProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeOfProduct(int? id, TypeOfProductDTO typeOfProductDTO)
        {
       
            if (id is null)
            {
                return BadRequest();
            }
            var typeOfProduct = await _context.TypeOfProducts.FindAsync(id);
            _context.Entry(typeOfProduct).State = EntityState.Modified;
         
            if(typeOfProduct is null) { return BadRequest(); }
            typeOfProduct.Name = typeOfProductDTO.Name;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfProductExists(typeOfProduct.Id))
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

        // POST: api/TypeOfProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeOfProduct>> PostTypeOfProduct(TypeOfProductDTO typeOfProductDTO)
        {
          if (_context.TypeOfProducts == null)
          {
              return Problem("Entity set 'LI2Context.TypeOfProducts'  is null.");
                
          }
            var typeOfProduct = new TypeOfProduct()
            {
                Id = typeOfProductDTO.Id,
                Name = typeOfProductDTO.Name,
            };
            _context.TypeOfProducts.Add(typeOfProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeOfProduct", new { id = typeOfProduct.Id }, typeOfProduct);
        }

        // DELETE: api/TypeOfProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfProduct(int id)
        {
            if (_context.TypeOfProducts == null)
            {
                return NotFound();
            }
            var typeOfProduct = await _context.TypeOfProducts.FindAsync(id);
            if (typeOfProduct == null)
            {
                return NotFound();
            }

            _context.TypeOfProducts.Remove(typeOfProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeOfProductExists(int id)
        {
            return (_context.TypeOfProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
