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
    public class PreferencesController : ControllerBase
    {
        private readonly ClotStoreContext _context;

        public PreferencesController(ClotStoreContext context)
        {
            _context = context;
        }

        // GET: api/Preferences
        [HttpGet("api/preferences")]
        public async Task<ActionResult<IEnumerable<Preference>>> GetPreferences()
        {
            if (_context.Preferences == null)
            {
                return NotFound();
            }
            return await _context.Preferences.ToListAsync();
        }

        // GET: api/Preferences/5
        [HttpGet("api/preferenceUsuari/")]
        public async Task<ActionResult<Preference>> GetPreferenceUsuari([FromQuery] int idUsuari)
        {
            if (_context.Preferences == null)
            {
                return NotFound();
            }
            var preference = _context.Preferences.Where(a=>a.IdUsuari == idUsuari).FirstOrDefault();

            if (preference == null)
            {
                return NotFound();
            }

            return preference;
        }

        // GET: api/Preferences/5
        [HttpGet("api/preference/")]
        public async Task<ActionResult<Preference>> GetPreference([FromQuery] int id)
        {
            if (_context.Preferences == null)
            {
                return NotFound();
            }
            var preference = await _context.Preferences.FindAsync(id);

            if (preference == null)
            {
                return NotFound();
            }

            return preference;
        }

        // POST: api/Preferences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/preference")]
        public async Task<ActionResult<Preference>> PostPreference([FromBody] Preference preference)
        {
            try
            {
                if (_context.Preferences == null)
                {
                    return Problem("Entity set 'ClotStoreContext.Preferences'  is null.");
                }
                var p = _context.Preferences.Where(a => a.IdPreferences == preference.IdPreferences).FirstOrDefault();
                //var idU = _context.Preferences.Where(a => a.IdUsuari == preference.IdUsuari).FirstOrDefault();
                if (p == null)
                {
                    _context.Preferences.Add(preference);
                }
                else
                {
                    p.Temporada = preference.Temporada;
                    p.Estil= preference.Estil;
                    p.Categoria = preference.Categoria;
                    p.IdColor = preference.IdColor;
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPreference", new { id = preference.IdPreferences }, preference);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        // DELETE: api/Preferences/5
        [HttpDelete("api/preference/")]
        public async Task<IActionResult> DeletePreference([FromQuery] int id)
        {
            if (_context.Preferences == null)
            {
                return NotFound();
            }
            var preference = await _context.Preferences.FindAsync(id);
            if (preference == null)
            {
                return NotFound();
            }

            _context.Preferences.Remove(preference);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreferenceExists(int id)
        {
            return (_context.Preferences?.Any(e => e.IdPreferences == id)).GetValueOrDefault();
        }
    }
}
