using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSWebApi.Models;
using System.Drawing;

namespace CSWebApi.Controllers
{
    [ApiController]
    public class ColorsProductesController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public ColorsProductesController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/ColorsProductes
        [HttpGet("api/colorsproductes")]
        public async Task<ActionResult<IEnumerable<ColorsProducte>>> GetColorsProducte()
        {
          if (_context.ColorsProducte == null)
          {
              return NotFound();
          }
            return await _context.ColorsProducte.ToListAsync();
        }

        // GET: api/ColorsProductes/5
        [HttpGet("api/colorsproducte/")]
        public async Task<ActionResult<IEnumerable<ColorsProducte>>> GetColorsProducte([FromQuery] int idProducte)
        {
          if (_context.ColorsProducte == null)
          {
              return NotFound();
          }
            var colorsProducte = await _context.ColorsProducte.Where(a=>a.IdProducte == idProducte).ToListAsync();

            if (colorsProducte == null)
            {
                return NotFound();
            }

            return colorsProducte;
        }

        // GET: api/ColorsProductes/5
        [HttpGet("api/colorproducte/")]
        public async Task<ActionResult<ColorsProducte>> GetColorsProducte([FromQuery] int idProducte, [FromQuery] int idColor)
        {
          if (_context.ColorsProducte == null)
          {
              return NotFound();
          }
            var colorsProducte =  _context.ColorsProducte.Where(a=>a.IdProducte == idProducte && a.IdColor == idColor).FirstOrDefault();

            if (colorsProducte == null)
            {
                return NotFound();
            }

            return colorsProducte;
        }

        // PUT: api/ColorsProductes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/colorproducte/")]
        public async Task<IActionResult> PutColorsProducte([FromQuery] int id, ColorsProducte colorsProducte)
        {
            if (id != colorsProducte.IdColor)
            {
                return BadRequest();
            }

            _context.Entry(colorsProducte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorsProducteExists(id))
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

        // POST: api/ColorsProductes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/colorproducte")]
        public async Task<ActionResult<ColorsProducte>> PostColorsProducte([FromQuery] int idColor, [FromQuery] int idProducte)
        {
            ColorsProducte colorsProducte = new ColorsProducte(idColor, idProducte);
            if (_context.ColorsProducte == null)
            {
                return Problem("Entity set 'ClotStoreContext.ColorsProducte'  is null.");
            }
            _context.ColorsProducte.Add(colorsProducte);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColorsProducteExists(colorsProducte.IdColor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColorsProducte", new { id = colorsProducte.IdColor }, colorsProducte);
        }

        // DELETE: api/ColorsProductes/5
        [HttpDelete("api/colorproducte/")]
        public async Task<IActionResult> DeleteColorsProducte([FromQuery] int id)
        {
            if (_context.ColorsProducte == null)
            {
                return NotFound();
            }
            var colorsProducte = await _context.ColorsProducte.FindAsync(id);
            if (colorsProducte == null)
            {
                return NotFound();
            }

            _context.ColorsProducte.Remove(colorsProducte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorsProducteExists(int? id)
        {
            return (_context.ColorsProducte?.Any(e => e.IdColor == id)).GetValueOrDefault();
        }
    }
}
