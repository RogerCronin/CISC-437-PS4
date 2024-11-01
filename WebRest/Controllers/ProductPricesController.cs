
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
    public class ProductPricesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public ProductPricesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> GetProductPrices()
        {
            return await _context.ProductPrices.ToListAsync();
        }

        // GET: api/ProductPrices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPrice>> GetProductPrice(string id)
        {
            var item = await _context.ProductPrices.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/ProductPrices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPrice(string id, ProductPrice item)
        {
            if (id != item.ProductPriceId)
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

        // POST: api/ProductPrice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductPrice>> PostProductPrice(ProductPrice item)
        {
            _context.ProductPrices.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ProductPriceId }, item);
        }

        // DELETE: api/ProductPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductPrice(string id)
        {
            var item = await _context.ProductPrices.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.ProductPrices.Any(e => e.ProductPriceId == id);
        }
    }
}
