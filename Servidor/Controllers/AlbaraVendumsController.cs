using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("albaraVenda")]
    [ApiController]
    public class AlbaraVendumsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public AlbaraVendumsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/AlbaraVendums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbaraVendum>>> GetAlbaraVenda()
        {
          if (_context.AlbaraVenda == null)
          {
              return NotFound();
          }
            return await _context.AlbaraVenda.ToListAsync();
        }

        // GET: api/AlbaraVendums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbaraVendum>> GetAlbaraVendum(int id)
        {
          if (_context.AlbaraVenda == null)
          {
              return NotFound();
          }
            var albaraVendum = await _context.AlbaraVenda.FindAsync(id);

            if (albaraVendum == null)
            {
                return NotFound();
            }

            return albaraVendum;
        }

        // PUT: api/AlbaraVendums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbaraVendum(int id, AlbaraVendum albaraVendum)
        {
            if (id != albaraVendum.IdAlbara)
            {
                return BadRequest();
            }

            _context.Entry(albaraVendum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbaraVendumExists(id))
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

        // POST: api/AlbaraVendums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlbaraVendum>> PostAlbaraVendum(AlbaraVendum albaraVendum)
        {
          if (_context.AlbaraVenda == null)
          {
              return Problem("Entity set 'DbProjecteContext.AlbaraVenda'  is null.");
          }
            _context.AlbaraVenda.Add(albaraVendum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbaraVendum", new { id = albaraVendum.IdAlbara }, albaraVendum);
        }

        // DELETE: api/AlbaraVendums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbaraVendum(int id)
        {
            if (_context.AlbaraVenda == null)
            {
                return NotFound();
            }
            var albaraVendum = await _context.AlbaraVenda.FindAsync(id);
            if (albaraVendum == null)
            {
                return NotFound();
            }

            _context.AlbaraVenda.Remove(albaraVendum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbaraVendumExists(int id)
        {
            return (_context.AlbaraVenda?.Any(e => e.IdAlbara == id)).GetValueOrDefault();
        }
    }
}
