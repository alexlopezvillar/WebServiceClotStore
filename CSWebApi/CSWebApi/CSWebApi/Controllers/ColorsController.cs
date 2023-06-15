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
    public class ColorsController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public ColorsController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Colors
        [HttpGet("api/colors")]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
          if (_context.Colors == null)
          {
              return NotFound();
          }
            return await _context.Colors.ToListAsync();
        }

        // GET: api/Colors/5
        [HttpGet("api/color/")]
        public async Task<ActionResult<Color>> GetColor([FromQuery] int id)
        {
          if (_context.Colors == null)
          {
              return NotFound();
          }
            var color = await _context.Colors.FindAsync(id);

            if (color == null)
            {
                return NotFound();
            }

            return color;
        }

        // PUT: api/Colors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/color/")]
        public async Task<IActionResult> PutColor([FromQuery] int id, Color color)
        {
            if (id != color.IdColor)
            {
                return BadRequest();
            }

            _context.Entry(color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
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

        // POST: api/Colors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/color")]
        public async Task<ActionResult<Color>> PostColor([FromBody] string nom)
        {
            Color color = new Color(nom);
          if (_context.Colors == null)
          {
              return Problem("Entity set 'ClotStoreContext.Colors'  is null.");
          }
            _context.Colors.Add(color);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColorExists(color.IdColor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColor", new { id = color.IdColor }, color);
        }

        // DELETE: api/Colors/5
        [HttpDelete("api/color/")]
        public async Task<IActionResult> DeleteColor([FromQuery] int id)
        {
            if (_context.Colors == null)
            {
                return NotFound();
            }
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorExists(int id)
        {
            return (_context.Colors?.Any(e => e.IdColor == id)).GetValueOrDefault();
        }
    }
}
