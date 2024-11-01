
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
    public class ProductStatusesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public ProductStatusesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> GetProductStatuses()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        // GET: api/ProductStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStatus>> GetProductStatus(string id)
        {
            var item = await _context.ProductStatuses.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/ProductStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductStatus(string id, ProductStatus item)
        {
            if (id != item.ProductStatusId)
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

        // POST: api/ProductStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> PostProductStatus(ProductStatus item)
        {
            _context.ProductStatuses.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ProductStatusId }, item);
        }

        // DELETE: api/ProductStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductStatus(string id)
        {
            var item = await _context.ProductStatuses.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}
