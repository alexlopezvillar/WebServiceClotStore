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
    public class ProducteCarretsController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public ProducteCarretsController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/ProducteCarrets
        [HttpGet("api/productecarrets")]
        public async Task<ActionResult<IEnumerable<ProducteCarret>>> GetProducteCarrets()
        {
          if (_context.ProducteCarrets == null)
          {
              return NotFound();
          }
            return await _context.ProducteCarrets.ToListAsync();
        }

        // GET: api/ProducteCarrets/5
        [HttpGet("api/quantProCarret/")]
        public async Task<ActionResult<int>> GetQuantitatProducteCarret([FromQuery] int idCarret, int idProducte, string nomTalla)
        {
            try
            {
                if (_context.ProducteCarrets == null)
                {
                    return NotFound();
                }
                var idTalla = _context.Talla.Where(a => a.Nom.Equals(nomTalla)).Select(a => a.IdTalla).FirstOrDefault();
                var tallaProd = await _context.TallasProducte.Where(a => a.IdProducte == idProducte && a.IdTalla == idTalla).Select(a => a.IdTallaProducte).FirstOrDefaultAsync();
                if (tallaProd == 0)
                {
                    return 0;
                }
                var producteCarret = await _context.ProducteCarrets.Where(a => a.IdCarret == idCarret && a.IdTallaProducte == tallaProd).FirstOrDefaultAsync();

                if (producteCarret == null)
                {
                    return 0;
                }

                return producteCarret.Quantitat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            
        }

        // GET: api/ProducteCarrets/5
        [HttpGet("api/productescarret/")]
        public async Task<ActionResult<IEnumerable<ProducteCarret>>> GetProductesCarret([FromQuery] int idCarret)
        {
          if (_context.ProducteCarrets == null)
          {
              return NotFound();
          }
            var producteCarret = await _context.ProducteCarrets.Where(a=>a.IdCarret == idCarret).ToListAsync();

            if (producteCarret == null)
            {
                return NotFound();
            }

            return producteCarret;
        }

        // GET: api/ProducteCarrets/5
        [HttpGet("api/productecarret/")]
        public async Task<ActionResult<ProducteCarret>> GetProducteCarret([FromQuery] int idCarret, [FromQuery] int idTallaProducte)
        {
          if (_context.ProducteCarrets == null)
          {
              return NotFound();
          }
            var producteCarret = _context.ProducteCarrets.Where(a=>a.IdTallaProducte == idTallaProducte && a.IdCarret == idCarret).FirstOrDefault();

            if (producteCarret == null)
            {
                return NotFound();
            }

            return producteCarret;
        }

        // GET: api/ProducteCarrets/5
        [HttpGet("api/factura/")]
        public async Task<ActionResult<IEnumerable<ProducteFactura>>> GetProducteCarretFactura([FromQuery] int idCarret)
        {
            List<ProducteFactura> lpf = new List<ProducteFactura>();
            if (_context.ProducteCarrets == null)//foto, nombre, precio, cantidad
            {
                return NotFound();
            }
            List<ProducteCarret> producteCarret = await _context.ProducteCarrets.Where(a=>a.IdCarret == idCarret).ToListAsync();

            if (producteCarret == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in producteCarret)
                {
                    var buscarIdProducte = _context.TallasProducte.Where(a=>a.IdTallaProducte == item.IdTallaProducte).Select(a=>a.IdProducte).FirstOrDefault();
                    if (buscarIdProducte == null || buscarIdProducte == 0)
                    {
                        return null;
                    }
                    Producte a = await _context.Productes.FindAsync(buscarIdProducte);
                    if (a != null)
                    {
                        lpf.Add(new ProducteFactura(a.IdProducte, item.IdTallaProducte, a.Nom, a.Imatge, (a.Preu*item.Quantitat), item.Quantitat));
                    }
                }
            }

            return lpf;
        }

        // PUT: api/ProducteCarrets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/productecarret/")]
        public async Task<ActionResult<bool>> PutProducteCarret([FromQuery] int idCarret, [FromQuery] int idTallaProducte, [FromQuery] bool subir)
        {
            try
            {
                var proCarr = await _context.ProducteCarrets.Where(a => a.IdCarret == idCarret && a.IdTallaProducte == idTallaProducte).FirstOrDefaultAsync();
                if (proCarr != null)
                {
                    if (subir)
                    {
                        proCarr.Quantitat += 1;
                    }
                    else
                    {
                        proCarr.Quantitat -= 1;
                    }
                    _context.Entry(proCarr).State = EntityState.Modified;

                    //actualizar carro
                    var buscarIdProducte = _context.TallasProducte.Where(a => a.IdTallaProducte == idTallaProducte).Select(a => a.IdProducte).FirstOrDefault();
                    if (buscarIdProducte == null || buscarIdProducte == 0)
                    {
                        return null;
                    }
                    var produ = _context.Productes.Where(a => a.IdProducte == buscarIdProducte).FirstOrDefault();
                    var carro = _context.Carrets.Where(a => a.IdCarret == idCarret).FirstOrDefault();
                    if (subir)
                    {
                        carro.PreuTotal += produ.Preu;
                    }
                    else
                    {
                        carro.PreuTotal -= produ.Preu;
                    }
                    if (carro.PreuTotal < 0)
                    {
                        carro.PreuTotal = 0;
                    }
                    _context.Entry(carro).State = EntityState.Modified;

                    if (proCarr.Quantitat == 0)
                    {
                        _context.ProducteCarrets.Remove(proCarr);
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return NoContent();
        }

        // POST: api/ProducteCarrets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/productecarret")]
        public async Task<ActionResult<ProducteCarret>> PostProducteCarret([FromQuery] int idCarret, [FromQuery] int idTallaProducte, [FromQuery] int quantitat)
        {
            try
            {
                ProducteCarret producteCarret = new ProducteCarret(idCarret, idTallaProducte, quantitat);
                ProducteCarret pc = null;
                if (_context.ProducteCarrets == null)
                {
                    return Problem("Entity set 'ClotStoreContext.ProducteCarrets'  is null.");
                }
                
                try
                {
                    if (ProducteCarretExists(producteCarret.IdCarret, producteCarret.IdTallaProducte))
                    {
                        pc = await _context.ProducteCarrets.Where(a => a.IdCarret == idCarret && a.IdTallaProducte == idTallaProducte).FirstOrDefaultAsync();
                        if (pc == null)
                        {
                            return null;
                        }
                        pc.Quantitat += quantitat;
                        _context.Entry(pc).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.ProducteCarrets.Add(producteCarret);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                pc = await _context.ProducteCarrets.Where(a => a.IdCarret == idCarret && a.IdTallaProducte == idTallaProducte).FirstOrDefaultAsync();
                if (pc == null)
                {
                    return null;
                }
                var buscarIdProducte = _context.TallasProducte.Where(a => a.IdTallaProducte == pc.IdTallaProducte).Select(a => a.IdProducte).FirstOrDefault();
                if (buscarIdProducte == null || buscarIdProducte == 0)
                {
                    return null;
                }
                var produ = _context.Productes.Where(a => a.IdProducte == buscarIdProducte).FirstOrDefault();
                var carro = _context.Carrets.Where(a => a.IdCarret == idCarret).FirstOrDefault();
                carro.PreuTotal += produ.Preu * quantitat;
                _context.Entry(carro).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProducteCarret", new { id = producteCarret.IdCarret }, producteCarret);
            }
            catch (Exception ex) //{"An exception was thrown while attempting to evaluate a LINQ query parameter expression. See the inner exception
                                 //for more information. To show additional information call 'DbContextOptionsBuilder.EnableSensitiveDataLogging'."}
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        // DELETE: api/ProducteCarrets/5
        [HttpDelete("api/productecarretAll/")]
        public async Task<ActionResult<bool>> DeleteAll([FromQuery] int idCarret)
        {
            try
            {
                if (_context.ProducteCarrets == null)
                {
                    return false;
                }
                var producteCarret = await _context.ProducteCarrets.Where(a => a.IdCarret == idCarret).ToListAsync();
                if (producteCarret == null)
                {
                    return false;
                }
                foreach (var item in producteCarret)
                {
                    var tallaProducte = await _context.TallasProducte.Where(a => a.IdTallaProducte == item.IdTallaProducte).FirstOrDefaultAsync();
                    if (tallaProducte == null)
                    {
                        return false;
                    }
                    else
                    {
                        tallaProducte.existencies -= item.Quantitat;
                        _context.Entry(tallaProducte).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                _context.ProducteCarrets.RemoveRange(producteCarret);
                var carro = _context.Carrets.Where(a => a.IdCarret == idCarret).FirstOrDefault();
                if (carro != null)
                {
                    carro.PreuTotal = 0;
                    _context.Entry(carro).State = EntityState.Modified;
                }
                else
                {
                    return false;
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

        // DELETE: api/ProducteCarrets/5
        [HttpDelete("api/productecarret/")]
        public async Task<IActionResult> DeleteProducteCarret([FromQuery] int id)
        {
            if (_context.ProducteCarrets == null)
            {
                return NotFound();
            }
            var producteCarret = await _context.ProducteCarrets.FindAsync(id);
            if (producteCarret == null)
            {
                return NotFound();
            }

            _context.ProducteCarrets.Remove(producteCarret);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProducteCarretExists(int? idCarret, int? idTallaProducte)
        {
            return (_context.ProducteCarrets?.Any(e => e.IdCarret == idCarret && e.IdTallaProducte == idTallaProducte)).GetValueOrDefault();
        }
    }
}
