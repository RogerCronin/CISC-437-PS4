item = "ProductStatus"
item_plural = "ProductStatuses"

file_template = """
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
    public class $$ItemPlural$$Controller : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public $$ItemPlural$$Controller(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/$$ItemPlural$$
        [HttpGet]
        public async Task<ActionResult<IEnumerable<$$Item$$>>> Get$$ItemPlural$$()
        {
            return await _context.$$ItemPlural$$.ToListAsync();
        }

        // GET: api/$$ItemPlural$$/5
        [HttpGet("{id}")]
        public async Task<ActionResult<$$Item$$>> Get$$Item$$(string id)
        {
            var item = await _context.$$ItemPlural$$.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/$$ItemPlural$$/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put$$Item$$(string id, $$Item$$ item)
        {
            if (id != item.$$Item$$Id)
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

        // POST: api/$$Item$$
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<$$Item$$>> Post$$Item$$($$Item$$ item)
        {
            _context.$$ItemPlural$$.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.$$Item$$Id }, item);
        }

        // DELETE: api/$$ItemPlural$$/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete$$Item$$(string id)
        {
            var item = await _context.$$ItemPlural$$.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.$$ItemPlural$$.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.$$ItemPlural$$.Any(e => e.$$Item$$Id == id);
        }
    }
}
"""

contents = file_template.replace("$$Item$$", item).replace("$$ItemPlural$$", item_plural)

new_file = open(item_plural + "Controller.cs", "w")
new_file.write(contents)
new_file.close()
