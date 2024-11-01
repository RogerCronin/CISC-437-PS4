
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
    public class OrderStatesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrderStatesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> GetOrderStates()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderState>> GetOrderState(string id)
        {
            var item = await _context.OrderStates.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/OrderStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderState(string id, OrderState item)
        {
            if (id != item.OrderStateId)
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

        // POST: api/OrderState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderState>> PostOrderState(OrderState item)
        {
            _context.OrderStates.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.OrderStateId }, item);
        }

        // DELETE: api/OrderStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderState(string id)
        {
            var item = await _context.OrderStates.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.OrderStates.Any(e => e.OrderStateId == id);
        }
    }
}
