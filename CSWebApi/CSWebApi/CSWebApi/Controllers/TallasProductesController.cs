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
    public class TallasProductesController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public TallasProductesController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/TallasProductes
        [HttpGet("api/tallasproductes")]
        public async Task<ActionResult<IEnumerable<TallasProducte>>> GetTallasProducte()
        {
          if (_context.TallasProducte == null)
          {
              return NotFound();
          }
            return await _context.TallasProducte.ToListAsync();
        }

        // GET: api/TallasProductes/5
        [HttpGet("api/tallasproducte/")]
        public async Task<ActionResult<IEnumerable<TallasProducte>>> GetTallasProducte([FromQuery] int idProducte)
        {
          if (_context.TallasProducte == null)
          {
              return NotFound();
          }
            var tallasProducte = await _context.TallasProducte.Where(a=>a.IdProducte == idProducte).ToListAsync();

            if (tallasProducte == null)
            {
                return NotFound();
            }

            return tallasProducte;
        }

        // GET: api/TallasProductes/5
        [HttpGet("api/idTallaProducte/")]
        public async Task<ActionResult<int>> GetIdTallaProducte([FromQuery] int idProducte, [FromQuery] string nomTalla)
        {
            try
            {
                if (_context.TallasProducte == null)
                {
                    return NotFound();
                }
                var talla = _context.Talla.Where(a => a.Nom.Equals(nomTalla)).FirstOrDefault();
                var tallasProducte = await _context.TallasProducte.Where(a => a.IdProducte == idProducte && a.IdTalla == talla.IdTalla).FirstOrDefaultAsync();

                if (tallasProducte == null)
                {
                    return 0;
                }

                return tallasProducte.IdTallaProducte;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        // GET: api/TallasProductes/5
        [HttpGet("api/tallaproducte/")]
        public async Task<ActionResult<int>> GetTallasProducteExistencia([FromQuery] int idProducte, [FromQuery] string nomTalla)
        {
            try
            {
                if (_context.TallasProducte == null)
                {
                    return NotFound();
                }
                var talla = _context.Talla.Where(a => a.Nom.Equals(nomTalla)).FirstOrDefault();
                var tallasProducte = await _context.TallasProducte.Where(a => a.IdProducte == idProducte && a.IdTalla == talla.IdTalla).FirstOrDefaultAsync();

                if (tallasProducte == null)
                {
                    return 0;
                }

                return tallasProducte.existencies;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        // GET: api/TallasProductes/5
        [HttpGet("api/existenciesTallaProducte/")]
        public async Task<ActionResult<int>> GetTPExistencia([FromQuery] int idTallaProducte)
        {
            try
            {
                if (_context.TallasProducte == null)
                {
                    return NotFound();
                }
                var tallasProducte = await _context.TallasProducte.Where(a => a.IdTallaProducte == idTallaProducte).FirstOrDefaultAsync();

                if (tallasProducte == null)
                {
                    return 0;
                }

                return tallasProducte.existencies;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        // PUT: api/TallasProductes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/tallaproducte/")]
        public async Task<ActionResult<bool>> PostTallasProducte([FromQuery] int idProducte, [FromQuery] string nomTalla, [FromQuery] int existencias)
        {
            try
            {
                var idTalla = _context.Talla.Where(a => a.Nom.Equals(nomTalla)).Select(a=>a.IdTalla).FirstOrDefault();
                if (idTalla == 0)
                {
                    return false;
                }
                var tallaProducte = await _context.TallasProducte.Where(a => a.IdProducte == idProducte && a.IdTalla == idTalla).FirstOrDefaultAsync();
                if (tallaProducte == null)
                {
                    TallasProducte tp = new TallasProducte(idTalla, idProducte, existencias);
                    _context.TallasProducte.Add(tp);
                }
                else
                {
                    tallaProducte.existencies += existencias;
                    _context.Entry(tallaProducte).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // DELETE: api/TallasProductes/5
        [HttpDelete("api/tallaproducte/")]
        public async Task<IActionResult> DeleteTallasProducte([FromQuery] int id)
        {
            if (_context.TallasProducte == null)
            {
                return NotFound();
            }
            var tallasProducte = await _context.TallasProducte.FindAsync(id);
            if (tallasProducte == null)
            {
                return NotFound();
            }

            _context.TallasProducte.Remove(tallasProducte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TallasProducteExists(int id)
        {
            return (_context.TallasProducte?.Any(e => e.IdTalla == id)).GetValueOrDefault();
        }
    }
}
