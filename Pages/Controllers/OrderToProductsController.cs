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
    public class OrderToProductsController : ControllerBase
    {
        private readonly LI2Context _context;

        public OrderToProductsController(LI2Context context)
        {
            _context = context;
        }

        // GET: api/OrderToProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToProduct>>> GetOrderToProducts()
        {
          if (_context.OrderToProducts == null)
          {
              return NotFound();
          }
            return await _context.OrderToProducts.ToListAsync();
        }

        // GET: api/OrderToProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToProduct>> GetOrderToProduct(int id)
        {
          if (_context.OrderToProducts == null)
          {
              return NotFound();
          }
            var orderToProduct = await _context.OrderToProducts.FindAsync(id);

            if (orderToProduct == null)
            {
                return NotFound();
            }

            return orderToProduct;
        }

        // PUT: api/OrderToProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderToProduct(int? id, OrderToProductDTO orderToProductDTO)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var orderToProduct = await _context.OrderToProducts.FindAsync(id);
            if (orderToProduct is null) { return BadRequest(); }
            var Orders = await _context.Orders.Where(Order => Order.Id == orderToProduct.OrderId).ToListAsync();
            _context.Entry(orderToProduct).State = EntityState.Modified;
            orderToProduct.Order = Orders.FirstOrDefault();
            orderToProduct.OrderId= orderToProductDTO.OrderId;
            orderToProduct.Price = orderToProductDTO.Price;
            orderToProduct.Name = orderToProductDTO.Name;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderToProductExists(orderToProduct.Id))
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

        // POST: api/OrderToProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderToProduct>> PostOrderToProduct(OrderToProductDTO orderToProductDTO)
        {
          if (_context.OrderToProducts == null)
          {
              return Problem("Entity set 'LI2Context.OrderToProducts'  is null.");
          }
          var Orders = await _context.Orders.Where(Order => Order.Id == orderToProductDTO.OrderId).ToListAsync();
            var orderToProduct = new OrderToProduct()
            {
                Id = orderToProductDTO.Id,
                Name = orderToProductDTO.Name,
                Price = orderToProductDTO.Price,
                OrderId = orderToProductDTO.OrderId,
                Order = Orders.FirstOrDefault()
            };
            _context.OrderToProducts.Add(orderToProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderToProduct", new { id = orderToProduct.Id }, orderToProduct);
        }

        // DELETE: api/OrderToProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderToProduct(int id)
        {
            if (_context.OrderToProducts == null)
            {
                return NotFound();
            }
            var orderToProduct = await _context.OrderToProducts.FindAsync(id);
            if (orderToProduct == null)
            {
                return NotFound();
            }

            _context.OrderToProducts.Remove(orderToProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderToProductExists(int id)
        {
            return (_context.OrderToProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
