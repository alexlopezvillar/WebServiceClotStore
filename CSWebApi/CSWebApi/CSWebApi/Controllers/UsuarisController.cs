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
    public class UsuarisController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public UsuarisController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Usuaris
        [HttpGet("api/usuaris")]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsuaris()
        {
          if (_context.Usuaris == null)
          {
              return NotFound();
          }
            return await _context.Usuaris.ToListAsync();
        }

        // GET: api/Usuaris/5
        [HttpGet("api/usuari/id/")]
        public async Task<ActionResult<Usuari>> GetUsuari([FromQuery] int id)
        {
          if (_context.Usuaris == null)
          {
              return NotFound();
          }
            var usuari = await _context.Usuaris.FindAsync(id);

            if (usuari == null)
            {
                return NotFound();
            }

            return usuari;
        }
        // GET: api/Usuaris/5
        [HttpGet("api/existeUsuari/")]
        public async Task<ActionResult<bool>> GetNomUsuariExistent([FromQuery] string nom)
        {
            if (_context.Usuaris == null)
            {
                return NotFound();
            }
            var usuari = _context.Usuaris.Where(a => a.Nom == nom).FirstOrDefault();

            if (usuari == null)
            {
                return false;
            }

            return true;
        }

        // GET: api/Usuaris/5
        [HttpGet("api/loginUsuari/")]
        public async Task<ActionResult<Usuari>> GetComprovarUsuari([FromQuery] string nom, [FromQuery] string contra)
        {
            try
            {
                if (_context.Usuaris == null)
                {
                    return NotFound();
                }
                var usuari = _context.Usuaris.Where(a => a.Nom == nom && a.Contra == contra).FirstOrDefault();

                if (usuari == null)
                {
                    return null;
                }

                return usuari;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        // GET: api/Usuaris/5
        [HttpGet("api/idUsuari/")]
        public async Task<ActionResult<int>> GetComprovarUsuari([FromQuery] string nom)
        {
          if (_context.Usuaris == null)
          {
              return NotFound();
          }
            var usuari = _context.Usuaris.Where(a=>a.Nom == nom).FirstOrDefault();

            if (usuari == null)
            {
                return null;
            }

            return usuari.IdUsuari;
        }


        // PUT: api/Usuaris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/usuari/")]
        public async Task<IActionResult> PutUsuari([FromQuery] int id, Usuari usuari)
        {
            if (id != usuari.IdUsuari)
            {
                return BadRequest();
            }

            _context.Entry(usuari).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariExists(id))
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

        // POST: api/Usuaris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/usuari")]
        public async Task<ActionResult<Usuari>> PostUsuari([FromBody] Usuari usuari)
        {
            if (_context.Usuaris == null)
            {
                return Problem("Entity set 'ClotStoreContext.Usuaris'  is null.");
            }
            _context.Usuaris.Add(usuari);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuari", new { id = usuari.IdUsuari }, usuari);
        }

        // DELETE: api/Usuaris/5
        [HttpDelete("api/usuari/")]
        public async Task<IActionResult> DeleteUsuari([FromQuery] int id)
        {
            if (_context.Usuaris == null)
            {
                return NotFound();
            }
            var usuari = await _context.Usuaris.FindAsync(id);
            if (usuari == null)
            {
                return NotFound();
            }

            _context.Usuaris.Remove(usuari);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariExists(int id)
        {
            return (_context.Usuaris?.Any(e => e.IdUsuari == id)).GetValueOrDefault();
        }
    }
}
