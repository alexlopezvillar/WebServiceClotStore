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
    public class PrendasController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public PrendasController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Prendas
        [HttpGet("api/prendas")]
        public async Task<ActionResult<IEnumerable<Prenda>>> GetPrendas()
        {
          if (_context.Prendas == null)
          {
              return NotFound();
          }
            return await _context.Prendas.ToListAsync();
        }

        // GET: api/Prendas/5
        [HttpGet("api/prenda/")]
        public async Task<ActionResult<Prenda>> GetPrenda([FromQuery] int codi)
        {
          if (_context.Prendas == null)
          {
              return NotFound();
          }
            var prenda = await _context.Prendas.Where(a => a.IdPrenda == codi).FirstOrDefaultAsync();

            if (prenda == null)
            {
                return NotFound();
            }

            return prenda;
        }

        // GET: api/Prendas/5
        [HttpGet("api/prendaNom/")]
        public async Task<ActionResult<Prenda>> GetPrenda([FromQuery] string nom)
        {
          if (_context.Prendas == null)
          {
              return NotFound();
          }
            var prenda = await _context.Prendas.Where(a => a.Nom.Equals(nom)).FirstOrDefaultAsync();

            if (prenda == null)
            {
                return NotFound();
            }

            return prenda;
        }

        // PUT: api/Prendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/prenda/")]
        public async Task<IActionResult> PutPrenda([FromQuery] int id, Prenda prenda)
        {
            if (id != prenda.IdPrenda)
            {
                return BadRequest();
            }

            _context.Entry(prenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrendaExists(id))
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

        // POST: api/Prendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/prenda")]
        public async Task<ActionResult<Prenda>> PostPrenda([FromBody] Prenda prenda)
        {
            if (_context.Prendas == null)
            {
                return Problem("Entity set 'ClotStoreContext.Prendas'  is null.");
            }
            _context.Prendas.Add(prenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrenda", new { id = prenda.IdPrenda }, prenda);
        }

        // DELETE: api/Prendas/5
        [HttpDelete("api/prenda/")]
        public async Task<IActionResult> DeletePrenda([FromQuery] int id)
        {
            if (_context.Prendas == null)
            {
                return NotFound();
            }
            var prenda = await _context.Prendas.FindAsync(id);
            if (prenda == null)
            {
                return NotFound();
            }

            _context.Prendas.Remove(prenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrendaExists(int id)
        {
            return (_context.Prendas?.Any(e => e.IdPrenda == id)).GetValueOrDefault();
        }
    }
}
