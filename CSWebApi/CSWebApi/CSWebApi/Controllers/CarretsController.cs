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
    public class CarretsController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public CarretsController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Carrets
        [HttpGet("api/carrets")]
        public async Task<ActionResult<IEnumerable<Carret>>> GetCarrets()
        {
            if (_context.Carrets == null)
            {
                return NotFound();
            }
            return await _context.Carrets.ToListAsync();
        }

        // GET: api/Carrets/5
        [HttpGet("api/carretsUsuari/")]
        public async Task<ActionResult<IEnumerable<Carret>>> GetCarretsUsuari([FromQuery] int idUsuari)
        {
            if (_context.Carrets == null)
            {
                return NotFound();
            }
            var carret = await _context.Carrets.Where(a=>a.IdUsuari == idUsuari).ToListAsync();

            if (carret == null)
            {
                return NotFound();
            }

            return carret;
        }

        // GET: api/Carrets/5
        [HttpGet("api/carret/")]
        public async Task<ActionResult<Carret>> GetCarret([FromQuery] int idCarret)
        {
            if (_context.Carrets == null)
            {
                return NotFound();
            }
            var carret = await _context.Carrets.Where(a=>a.IdCarret == idCarret).FirstOrDefaultAsync();

            if (carret == null)
            {
                return NotFound();
            }

            return carret;
        }

        // GET: api/Carrets/5
        [HttpGet("api/carretPreu/")]
        public async Task<ActionResult<double>> GetCarretPreu([FromQuery] int idCarret)
        {
            if (_context.Carrets == null)
            {
                return NotFound();
            }
            var carret = await _context.Carrets.Where(a=>a.IdCarret == idCarret).Select(a=>a.PreuTotal).FirstOrDefaultAsync();

            if (carret == null)
            {
                return NotFound();
            }

            return carret;
        }

        // GET: api/Carrets/5
        [HttpGet("api/idCarret/")]
        public async Task<ActionResult<int>> GetIdCarret([FromQuery] int idUsuari)
        {
            if (_context.Carrets == null)
            {
                return NotFound();
            }
            var carret = await _context.Carrets.Where(a=>a.IdUsuari == idUsuari).FirstOrDefaultAsync();

            if (carret == null)
            {
                return NotFound();
            }

            return carret.IdCarret;
        }

        // PUT: api/Carrets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/carret/")]
        public async Task<IActionResult> PutCarret([FromQuery] int id, Carret carret)
        {
            if (id != carret.IdCarret)
            {
                return BadRequest();
            }

            _context.Entry(carret).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarretExists(id))
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

        // POST: api/Carrets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("api/carretCrear")]
        public async Task<ActionResult<Carret>> PostCarret([FromQuery] int idUsuari)
        {
            Carret carret = new Carret(0.0, idUsuari);
            if (_context.Carrets == null)
            {
                return Problem("Entity set 'ClotStoreContext.Carrets'  is null.");
            }
            _context.Carrets.Add(carret);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarret", new { id = carret.IdCarret }, carret);
        }


        // DELETE: api/Carrets/5
        [HttpDelete("api/carret/")]
        public async Task<IActionResult> DeleteCarret([FromQuery] int id)
        {
            if (_context.Carrets == null)
            {
                return NotFound();
            }
            var carret = await _context.Carrets.FindAsync(id);
            if (carret == null)
            {
                return NotFound();
            }

            _context.Carrets.Remove(carret);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarretExists(int id)
        {
            return (_context.Carrets?.Any(e => e.IdCarret == id)).GetValueOrDefault();
        }
    }
}
