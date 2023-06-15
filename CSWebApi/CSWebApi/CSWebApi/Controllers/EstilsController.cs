using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSWebApi.Models;

namespace CSWebApi.Controllers
{
    [ApiController]
    public class EstilsController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public EstilsController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Estils
        [HttpGet("api/estils")]
        public async Task<ActionResult<IEnumerable<Estil>>> GetEstils()
        {
          if (_context.Estils == null)
          {
              return NotFound();
          }
            return await _context.Estils.ToListAsync();
        }

        // GET: api/Estils/5
        [HttpGet("api/estil/")]
        public async Task<ActionResult<Estil>> GetEstil([FromQuery] int codi)
        {
            if (_context.Estils == null)
            {
                return NotFound();
            }
            var estil = await _context.Estils.Where(a => a.IdEstil == codi).FirstOrDefaultAsync();

            if (estil == null)
            {
                return NotFound();
            }

            return estil;
        }

        // GET: api/Estils/5
        [HttpGet("api/estilNom/")]
        public async Task<ActionResult<Estil>> GetEstil([FromQuery] string nom)
        {
            if (_context.Estils == null)
            {
                return NotFound();
            }
            var estil = await _context.Estils.Where(a => a.Nom.Equals(nom)).FirstOrDefaultAsync();

            if (estil == null)
            {
                return NotFound();
            }

            return estil;
        }

        // PUT: api/Estils/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/estil/")]
        public async Task<IActionResult> PutEstil([FromQuery] int id, Estil estil)
        {
            if (id != estil.IdEstil)
            {
                return BadRequest();
            }

            _context.Entry(estil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstilExists(id))
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

        // POST: api/Estils
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/estil")]
        public async Task<ActionResult<Estil>> PostEstil([FromBody] Estil estil)
        {
            if (_context.Estils == null)
            {
                return Problem("Entity set 'ClotStoreContext.Estils'  is null.");
            }
            _context.Estils.Add(estil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstil", new { id = estil.IdEstil }, estil);
        }

        // DELETE: api/Estils/5
        [HttpDelete("api/estil/")]
        public async Task<IActionResult> DeleteEstil([FromQuery] int id)
        {
            if (_context.Estils == null)
            {
                return NotFound();
            }
            var estil = await _context.Estils.FindAsync(id);
            if (estil == null)
            {
                return NotFound();
            }

            _context.Estils.Remove(estil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstilExists(int id)
        {
            return (_context.Estils?.Any(e => e.IdEstil == id)).GetValueOrDefault();
        }
    }
}
