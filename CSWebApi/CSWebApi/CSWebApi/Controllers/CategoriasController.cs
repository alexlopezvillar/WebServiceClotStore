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
    public class CategoriasController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public CategoriasController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet("api/categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var a = await _context.Categorias.ToListAsync();
            return a;
        }

        // GET: api/Categorias/5
        [HttpGet("api/categoria/")]
        public async Task<ActionResult<Categoria>> GetCategoria([FromQuery] int codi)
        {
          if (_context.Categorias == null)
          {
              return NotFound();
          }
            var categoria = await _context.Categorias.Where(a => a.IdCategoria == codi).FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // GET: api/Categorias/5
        [HttpGet("api/categoriaNom/")]
        public async Task<ActionResult<Categoria>> GetCategoria([FromQuery] string nom)
        {
          if (_context.Categorias == null)
          {
              return NotFound();
          }
            var categoria = await _context.Categorias.Where(a => a.Nom.Equals(nom)).FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/categoria/")]
        public async Task<IActionResult> PutCategoria([FromQuery] int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/categoria")]
        public async Task<ActionResult<Categoria>> PostCategoria([FromBody] Categoria categoria)
        {
          if (_context.Categorias == null)
          {
              return Problem("Entity set 'ClotStoreContext.Categorias'  is null.");
          }
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
        }
        
        // DELETE: api/Categorias/5
        [HttpDelete("api/categoria/")]
        public async Task<IActionResult> DeleteCategoria([FromQuery] int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return (_context.Categorias?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }
    }
}
