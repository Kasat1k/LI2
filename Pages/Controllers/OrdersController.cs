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
    public class OrdersController : ControllerBase
    {
        private readonly LI2Context _context;

        public OrdersController(LI2Context context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int? id, OrderDTO orderDTO)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order is null) { return BadRequest(); }
            var statusOrders = await _context.StatusOfOrders.Where(Order => Order.Id == order.StatusOfOrderId).ToListAsync();

        
            order.Name = orderDTO.Name;
            order.StatusOfOrderId = orderDTO.StatusOfOrderId;
            order.StatusOfOrder = statusOrders.FirstOrDefault();
           
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDTO orderDTO)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'LI2Context.Orders'  is null.");
          }
          var statusOrders = await _context.StatusOfOrders.Where(Order => Order.Id == orderDTO.StatusOfOrderId).ToListAsync();

            var order = new Order()
            {
                Id = orderDTO.Id,
                Name = orderDTO.Name,
                StatusOfOrderId = orderDTO.StatusOfOrderId,
                StatusOfOrder = statusOrders.FirstOrDefault()
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
