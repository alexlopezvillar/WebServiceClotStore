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
    public class TemporadasController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public TemporadasController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Temporadas
        [HttpGet("api/temporadas")]
        public async Task<ActionResult<IEnumerable<Temporada>>> GetTemporadas()
        {
          if (_context.Temporadas == null)
          {
              return NotFound();
          }
            return await _context.Temporadas.ToListAsync();
        }

        // GET: api/Temporadas/5
        [HttpGet("api/temporada/")]
        public async Task<ActionResult<Temporada>> GetTemporada([FromQuery] int codi)
        {
          if (_context.Temporadas == null)
          {
              return NotFound();
          }
            var temporada = await _context.Temporadas.Where(a => a.IdTemporada == codi).FirstOrDefaultAsync();

            if (temporada == null)
            {
                return NotFound();
            }

            return temporada;
        }

        // GET: api/Temporadas/5
        [HttpGet("api/temporadaNom/")]
        public async Task<ActionResult<Temporada>> GetTemporada([FromQuery] string nom)
        {
          if (_context.Temporadas == null)
          {
              return NotFound();
          }
            var temporada = await _context.Temporadas.Where(a => a.Nom.Equals(nom)).FirstOrDefaultAsync();

            if (temporada == null)
            {
                return NotFound();
            }

            return temporada;
        }


        // PUT: api/Temporadas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/temporada/")]
        public async Task<IActionResult> PutTemporada([FromQuery] int id, Temporada temporada)
        {
            if (id != temporada.IdTemporada)
            {
                return BadRequest();
            }

            _context.Entry(temporada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporadaExists(id))
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

        // POST: api/Temporadas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/temporada")]
        public async Task<ActionResult<Temporada>> PostTemporada([FromBody] Temporada temporada)
        {
            if (_context.Temporadas == null)
            {
                return Problem("Entity set 'ClotStoreContext.Temporadas'  is null.");
            }
            _context.Temporadas.Add(temporada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemporada", new { id = temporada.IdTemporada }, temporada);
        }

        // DELETE: api/Temporadas/5
        [HttpDelete("api/temporada/")]
        public async Task<IActionResult> DeleteTemporada([FromQuery] int id)
        {
            if (_context.Temporadas == null)
            {
                return NotFound();
            }
            var temporada = await _context.Temporadas.FindAsync(id);
            if (temporada == null)
            {
                return NotFound();
            }

            _context.Temporadas.Remove(temporada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemporadaExists(int id)
        {
            return (_context.Temporadas?.Any(e => e.IdTemporada == id)).GetValueOrDefault();
        }
    }
}
