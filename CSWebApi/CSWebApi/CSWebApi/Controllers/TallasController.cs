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
    public class TallasController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public TallasController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Tallas
        [HttpGet("api/tallas")]
        public async Task<ActionResult<IEnumerable<Talla>>> GetTalla()
        {
          if (_context.Talla == null)
          {
              return NotFound();
          }
            return await _context.Talla.ToListAsync();
        }

        // GET: api/Tallas/5
        [HttpGet("api/talla/")]
        public async Task<ActionResult<Talla>> GetTalla([FromQuery] int id)
        {
          if (_context.Talla == null)
          {
              return NotFound();
          }
            var talla = await _context.Talla.FindAsync(id);

            if (talla == null)
            {
                return NotFound();
            }

            return talla;
        }

        // GET: api/Tallas/5
        [HttpGet("api/filtreTallas/")]
        public async Task<ActionResult<IEnumerable<Talla>>> GetTallas([FromQuery] Boolean isNumber)
        {
            if (_context.Talla == null)
            {
                return NotFound();
            }
            List<Talla> talla;
            if (isNumber)
            {
                talla = await _context.Talla.Where(a => a.IdTalla <= 16 && a.IdTalla >= 6).ToListAsync();
            }
            else
            {
                talla = await _context.Talla.Where(a => a.IdTalla <= 5).ToListAsync();
            }

            if (talla == null)
            {
                return NotFound();
            }

            return talla;
        }

        // PUT: api/Tallas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/talla/")]
        public async Task<IActionResult> PutTalla([FromQuery] int id, Talla talla)
        {
            if (id != talla.IdTalla)
            {
                return BadRequest();
            }

            _context.Entry(talla).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TallaExists(id))
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

        // POST: api/Tallas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/talla")]
        public async Task<ActionResult<Talla>> PostTalla(Talla talla)
        {
          if (_context.Talla == null)
          {
              return Problem("Entity set 'ClotStoreContext.Talla'  is null.");
          }
            _context.Talla.Add(talla);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTalla", new { id = talla.IdTalla }, talla);
        }

        // DELETE: api/Tallas/5
        [HttpDelete("api/talla/")]
        public async Task<IActionResult> DeleteTalla([FromQuery] int id)
        {
            if (_context.Talla == null)
            {
                return NotFound();
            }
            var talla = await _context.Talla.FindAsync(id);
            if (talla == null)
            {
                return NotFound();
            }

            _context.Talla.Remove(talla);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TallaExists(int id)
        {
            return (_context.Talla?.Any(e => e.IdTalla == id)).GetValueOrDefault();
        }
    }
}
