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
    public class ProductesController : ControllerBase
    {
        private readonly ClotStoreContext _context;
        public ProductesController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Productes
        [HttpGet("api/productes")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductes()
        {
            if (_context.Productes == null)
            {
                return NotFound();
            }
            return await _context.Productes.OrderByDescending(a => a.IdProducte).ToListAsync();
        }

        // GET: api/Productes
        [HttpGet("api/mascaro")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesCaro()
        {
            if (_context.Productes == null)
            {
                return NotFound();
            }
            return await _context.Productes.OrderByDescending(a => a.Preu).ToListAsync();
        }

        // GET: api/Productes
        [HttpGet("api/masbarato")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesBarato()
        {
            if (_context.Productes == null)
            {
                return NotFound();
            }
            return await _context.Productes.OrderBy(a => a.Preu).ToListAsync();
        }

        // GET: api/Productes/5
        [HttpGet("api/producte/")]
        public async Task<ActionResult<Producte>> GetProducte([FromQuery] int id)
        {
            try
            {
                if (_context.Productes == null)
                {
                    return NotFound();
                }
                var producte = await _context.Productes.FindAsync(id);

                if (producte == null)
                {
                    return NotFound();
                }

                return producte;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        // GET: api/Productes/5
        [HttpGet("api/producteCategoria/")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesCategoria([FromQuery] int idCategoria, [FromQuery] int idPrenda)
        {
          if (_context.Productes == null)
          {
              return NotFound();
          }
            var producte = await _context.Productes.Where(a => a.Categoria == idCategoria && a.Prenda == idPrenda).OrderByDescending(a => a.IdProducte).ToListAsync();

            if (producte == null)
            {
                return NotFound();
            }

            return producte;
        }

        // GET: api/Productes/5
        [HttpGet("api/productePrenda/")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesPrenda([FromQuery] int idPrenda)
        {
          if (_context.Productes == null)
          {
              return NotFound();
          }
            var producte = await _context.Productes.Where(a => a.Prenda == idPrenda).OrderByDescending(a => a.IdProducte).ToListAsync();

            if (producte == null)
            {
                return NotFound();
            }

            return producte;
        }

        // GET: api/Productes/5
        [HttpGet("api/producteMarca/")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesMarca([FromQuery] int idMarca, [FromQuery] int idPrenda)
        {
          if (_context.Productes == null)
          {
              return NotFound();
          }
            var producte = await _context.Productes.Where(a => a.Marca == idMarca && a.Prenda == idPrenda).OrderByDescending(a => a.IdProducte).ToListAsync();

            if (producte == null)
            {
                return NotFound();
            }

            return producte;
        }

        // GET: api/Productes/5
        [HttpGet("api/producteTemporada/")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesTemporada([FromQuery] int idTemporada, [FromQuery] int idPrenda)
        {
          if (_context.Productes == null)
          {
              return NotFound();
          }
            var producte = await _context.Productes.Where(a => a.Temporada == idTemporada && a.Prenda == idPrenda).OrderByDescending(a => a.IdProducte).ToListAsync();

            if (producte == null)
            {
                return NotFound();
            }

            return producte;
        }

        // GET: api/Productes/5
        [HttpGet("api/producteEstil/")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesEstil([FromQuery] int idEstil, [FromQuery] int idPrenda)
        {
          if (_context.Productes == null)
          {
              return NotFound();
          }
            var producte = await _context.Productes.Where(a => a.Estil == idEstil && a.Prenda == idPrenda).OrderByDescending(a=>a.IdProducte).ToListAsync();

            if (producte == null)
            {
                return NotFound();
            }

            return producte;
        }

        // GET: api/Productes/5
        [HttpGet("api/outfitFet/")]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProductesPreferences([FromQuery] int idPreferences)
        {
            try
            {
                if (_context.Productes == null)
                {
                    return NotFound();
                }
                var xTemp = 0;
                var yTemp = 0;
                var xCate = 0;
                var yCate = 0;
                var prefe = await _context.Preferences.Where(a => a.IdPreferences == idPreferences).FirstOrDefaultAsync();
                if (prefe != null)
                {
                    switch (prefe.Temporada)
                    {
                        case 1:
                        case 2:
                            xTemp = 1;
                            yTemp = 2;
                            break;
                        case 3:
                        case 4:
                            xTemp = 3;
                            yTemp = 4;
                            break;
                    }
                    switch (prefe.Categoria)
                    {
                        case 1:
                            xCate = 1;
                            yCate = 5;
                            break;
                        case 2:
                            xCate = 2;
                            yCate = 5;
                            break;
                    }
                    List<Producte> camisetas = await _context.Productes.Where(a => a.Estil == prefe.Estil && (a.Temporada == xTemp || a.Temporada == yTemp) && (a.Categoria == xCate || a.Categoria == yCate) && a.Prenda == 3).Include(a => a.ColorsProductes).ToListAsync();
                    List<Producte> pantalones = await _context.Productes.Where(a => a.Estil == prefe.Estil && (a.Temporada == xTemp || a.Temporada == yTemp) && (a.Categoria == xCate || a.Categoria == yCate) && a.Prenda == 2).Include(a => a.ColorsProductes).ToListAsync();
                    List<Producte> zapatos = await _context.Productes.Where(a => a.Estil == prefe.Estil && (a.Temporada == xTemp || a.Temporada == yTemp) && (a.Categoria == xCate || a.Categoria == yCate) && a.Prenda == 4).Include(a => a.ColorsProductes).ToListAsync();

                    List<Producte> enviar = new List<Producte>();


                    List<Producte> camisetaPorColor = new List<Producte>();
                    List<Producte> pantalonesPorColor = new List<Producte>();
                    List<Producte> zapatosPorColor = new List<Producte>();
                    bool boolColor;


                    if (camisetas.Count > 0 && pantalones.Count > 0 && zapatos.Count > 0)
                    {
                        foreach (var camiseta in camisetas)
                        {
                            var listaColor = camiseta.ColorsProductes;
                            boolColor = false;
                            foreach (var color in listaColor)
                            {
                                if (color.IdColor == prefe.IdColor)
                                {
                                    boolColor = true;
                                }
                            }
                            if (boolColor)
                            {
                                camisetaPorColor.Add(camiseta);
                                boolColor = false;
                            }
                        }
                        foreach (var pantalon in pantalones)
                        {
                            var listaColor = pantalon.ColorsProductes;
                            boolColor = false;
                            foreach (var color in listaColor)
                            {
                                if (color.IdColor == prefe.IdColor)
                                {
                                    boolColor = true;
                                }
                            }
                            if (boolColor)
                            {
                                pantalonesPorColor.Add(pantalon);
                                boolColor = false;
                            }
                        }
                        foreach (var zapato in zapatos)
                        {
                            boolColor = false;
                            foreach (var color in zapato.ColorsProductes)
                            {
                                if (color.IdColor == prefe.IdColor)
                                {
                                    boolColor = true;
                                }
                            }
                            if (boolColor)
                            {
                                zapatosPorColor.Add(zapato);
                                boolColor = false;
                            }
                        }


                        Random rand = new Random();
                        Producte cami;
                        Producte pant;
                        Producte zapa;
                        if (camisetaPorColor.Count > 0)
                        {
                            enviar.Add(camisetaPorColor[0]);
                        }
                        else
                        {
                            enviar.Add(camisetas[0]);
                        }

                        if (pantalonesPorColor.Count > 0)
                        {
                            enviar.Add(pantalonesPorColor[0]);
                        }
                        else
                        {
                            enviar.Add(pantalones[0]);
                        }

                        if (zapatosPorColor.Count > 0)
                        {
                            enviar.Add(zapatosPorColor[0]);
                        }
                        else
                        {
                            enviar.Add(zapatos[0]);
                        }

                        return enviar;
                    }
                    else
                    {
                        enviar.Add(new Producte("", 0.0, "", 1, 1, 1, 1, 1));
                        return enviar;
                    }
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        //Imagen
        [HttpGet("api/getImagen/")]
        public async Task<FileContentResult> GetFoto(string nomArxiu)
        {
            if (nomArxiu == null)
            {
                return null;
            }
            String ruta = Path.Combine(Environment.CurrentDirectory, "fotos", nomArxiu);
            byte[] byteImagen = System.IO.File.ReadAllBytes(ruta);
            return File(byteImagen, "image/jpg");
        }


        // PUT: api/Productes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/producte/")]
        public async Task<ActionResult<bool>> PutProducte([FromQuery] int idProducte, [FromQuery] string nom, [FromQuery] double preu)
        {
            try
            {
                var producte = _context.Productes.Find(idProducte);
                if (producte == null)
                {
                    return false;
                }
                producte.Nom = nom;
                producte.Preu = preu;
                _context.Entry(producte).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // POST: api/Productes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/producte")]
        public async Task<ActionResult<Producte>> PostProducte([FromQuery] string nom,
                                                                [FromQuery] double preu,
                                                                [FromQuery] int categoria,
                                                                [FromQuery] int prenda,
                                                                [FromQuery] int marca, 
                                                                [FromQuery] int temporada,
                                                                [FromQuery] int estil, 
                                                                [FromForm] IFormFile Archivo, [FromQuery] String nomArxiu)
        {
            String ruta = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "fotos" + Path.DirectorySeparatorChar + nomArxiu;
            try
            {
                if (_context.Productes == null)
                {
                    return Problem("Entity set 'ClotStoreContext.Productes'  is null.");
                }
                //imagen
                using (FileStream newFile = System.IO.File.Create(ruta))
                {
                    Archivo.CopyTo(newFile);
                    await newFile.FlushAsync();
                }
                string archivo = "http://172.16.24.115:45455/api/getImagen?nomArxiu=" + nomArxiu;
                Producte producte = new Producte(nom, preu, archivo, categoria, prenda, marca, temporada, estil);
                _context.Productes.Add(producte);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProducte", new { id = producte.IdProducte }, producte);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // DELETE: api/Productes/5
        [HttpDelete("api/producte/")]
        public async Task<IActionResult> DeleteProducte([FromQuery] int idProducte)
        {
            if (_context.Productes == null)
            {
                return NotFound();
            }
            var producte = await _context.Productes.FindAsync(idProducte);
            if (producte == null)
            {
                return NotFound();
            }

            _context.Productes.Remove(producte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProducteExists(int id)
        {
            return (_context.Productes?.Any(e => e.IdProducte == id)).GetValueOrDefault();
        }
    }
}
