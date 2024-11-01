
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersLinesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrdersLinesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrdersLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> GetOrdersLines()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrdersLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersLine>> GetOrdersLine(string id)
        {
            var item = await _context.OrdersLines.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/OrdersLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdersLine(string id, OrdersLine item)
        {
            if (id != item.OrdersLineId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/OrdersLine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> PostOrdersLine(OrdersLine item)
        {
            _context.OrdersLines.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.OrdersLineId }, item);
        }

        // DELETE: api/OrdersLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdersLine(string id)
        {
            var item = await _context.OrdersLines.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}
