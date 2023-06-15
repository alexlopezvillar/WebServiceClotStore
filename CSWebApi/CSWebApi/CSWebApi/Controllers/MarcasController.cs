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
    public class MarcasController : ControllerBase
    {
        private readonly ClotStoreContext _context;
        public MarcasController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Marcas
        [HttpGet("api/marcas")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
          if (_context.Marcas == null)
          {
              return NotFound();
          }
            return await _context.Marcas.ToListAsync();
        }

        // GET: api/Marcas/5
        [HttpGet("api/marca/")]
        public async Task<ActionResult<Marca>> GetMarca( [FromQuery] int id)
        {
            if (_context.Marcas == null)
            {
                return NotFound();
            }
            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return marca;
        }

        // GET: api/Marcas/5
        [HttpGet("api/marcaNom/")]
        public async Task<ActionResult<Marca>> GetMarca( [FromQuery] string nom)
        {
            if (_context.Marcas == null)
            {
                return NotFound();
            }
            var marca = await _context.Marcas.Where(a=>a.Nom.Equals(nom)).FirstOrDefaultAsync();

            if (marca == null)
            {
                return NotFound();
            }

            return marca;
        }

        // PUT: api/Marcas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/marca/")]
        public async Task<IActionResult> PutMarca([FromQuery] int id, Marca marca)
        {
            if (id != marca.IdMarca)
            {
                return BadRequest();
            }

            _context.Entry(marca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaExists(id))
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

        // POST: api/Marcas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/marca")]
        public async Task<ActionResult<Marca>> PostMarca([FromQuery] string nom, [FromForm] IFormFile Archivo, [FromQuery] String nomArxiu)
        {
            try
            {
                if (_context.Marcas == null)
                {
                    return Problem("Entity set 'ClotStoreContext.Marcas'  is null.");
                }
                String ruta = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "fotos" + Path.DirectorySeparatorChar + nomArxiu;
                using (FileStream newFile = System.IO.File.Create(ruta))
                {
                    Archivo.CopyTo(newFile);
                    await newFile.FlushAsync();
                }
                string getRuta = "http://172.16.24.115:45455/api/getImagen?nomArxiu=" + nomArxiu;

                Marca marca = new Marca(nom, getRuta);
                _context.Marcas.Add(marca);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMarca", new { id = marca.IdMarca }, marca);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        // DELETE: api/Marcas/5
        [HttpDelete("api/marca/")]
        public async Task<IActionResult> DeleteMarca([FromQuery] int idMarca)
        {
            if (_context.Marcas == null)
            {
                return NotFound();
            }
            var marca = await _context.Marcas.FindAsync(idMarca);
            if (marca == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarcaExists(int id)
        {
            return (_context.Marcas?.Any(e => e.IdMarca == id)).GetValueOrDefault();
        }
    }
}
