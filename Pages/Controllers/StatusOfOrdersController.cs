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
    public class StatusOfOrdersController : ControllerBase
    {
        private readonly LI2Context _context;

        public StatusOfOrdersController(LI2Context context)
        {
            _context = context;
        }

        // GET: api/StatusOfOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusOfOrder>>> GetStatusOfOrders()
        {
          if (_context.StatusOfOrders == null)
          {
              return NotFound();
          }
            return await _context.StatusOfOrders.ToListAsync();
        }

        // GET: api/StatusOfOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusOfOrder>> GetStatusOfOrder(int id)
        {
          if (_context.StatusOfOrders == null)
          {
              return NotFound();
          }
            var statusOfOrder = await _context.StatusOfOrders.FindAsync(id);

            if (statusOfOrder == null)
            {
                return NotFound();
            }

            return statusOfOrder;
        }

        // PUT: api/StatusOfOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusOfOrder(int? id, StatusOfOrderDTO statusOfOrderDTO)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var statusOfOrder = await _context.StatusOfOrders.FindAsync(id);
            if (statusOfOrder is null) { return BadRequest(); }
            statusOfOrder.Status = statusOfOrderDTO.Status;
            _context.Entry(statusOfOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusOfOrderExists(statusOfOrder.Id))
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

        // POST: api/StatusOfOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusOfOrder>> PostStatusOfOrder(StatusOfOrderDTO statusOfOrderDTO)
        {
          if (_context.StatusOfOrders == null)
          {
              return Problem("Entity set 'LI2Context.StatusOfOrders'  is null.");
          }
            var statusOfOrder = new StatusOfOrder()
            {
                Id = statusOfOrderDTO.Id,
                Status = statusOfOrderDTO.Status,
            };
            _context.StatusOfOrders.Add(statusOfOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusOfOrder", new { id = statusOfOrder.Id }, statusOfOrder);
        }

        // DELETE: api/StatusOfOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusOfOrder(int id)
        {
            if (_context.StatusOfOrders == null)
            {
                return NotFound();
            }
            var statusOfOrder = await _context.StatusOfOrders.FindAsync(id);
            if (statusOfOrder == null)
            {
                return NotFound();
            }

            _context.StatusOfOrders.Remove(statusOfOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusOfOrderExists(int id)
        {
            return (_context.StatusOfOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
