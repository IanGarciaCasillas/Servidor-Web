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
    [Route("albaraVendaDetall")]
    [ApiController]
    public class AlbaraVendaDetallsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public AlbaraVendaDetallsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/AlbaraVendaDetalls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbaraVendaDetall>>> GetAlbaraVendaDetalls()
        {
          if (_context.AlbaraVendaDetalls == null)
          {
              return NotFound();
          }
            return await _context.AlbaraVendaDetalls.ToListAsync();
        }

        // GET: api/AlbaraVendaDetalls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbaraVendaDetall>> GetAlbaraVendaDetall(int id)
        {
          if (_context.AlbaraVendaDetalls == null)
          {
              return NotFound();
          }
            var albaraVendaDetall = await _context.AlbaraVendaDetalls.FindAsync(id);

            if (albaraVendaDetall == null)
            {
                return NotFound();
            }

            return albaraVendaDetall;
        }

        // PUT: api/AlbaraVendaDetalls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbaraVendaDetall(int id, AlbaraVendaDetall albaraVendaDetall)
        {
            if (id != albaraVendaDetall.IdAlbaraVenda)
            {
                return BadRequest();
            }

            _context.Entry(albaraVendaDetall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbaraVendaDetallExists(id))
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

        // POST: api/AlbaraVendaDetalls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlbaraVendaDetall>> PostAlbaraVendaDetall(AlbaraVendaDetall albaraVendaDetall)
        {
          if (_context.AlbaraVendaDetalls == null)
          {
              return Problem("Entity set 'DbProjecteContext.AlbaraVendaDetalls'  is null.");
          }
            _context.AlbaraVendaDetalls.Add(albaraVendaDetall);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlbaraVendaDetallExists(albaraVendaDetall.IdAlbaraVenda))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAlbaraVendaDetall", new { id = albaraVendaDetall.IdAlbaraVenda }, albaraVendaDetall);
        }

        // DELETE: api/AlbaraVendaDetalls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbaraVendaDetall(int id)
        {
            if (_context.AlbaraVendaDetalls == null)
            {
                return NotFound();
            }
            var albaraVendaDetall = await _context.AlbaraVendaDetalls.FindAsync(id);
            if (albaraVendaDetall == null)
            {
                return NotFound();
            }

            _context.AlbaraVendaDetalls.Remove(albaraVendaDetall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbaraVendaDetallExists(int id)
        {
            return (_context.AlbaraVendaDetalls?.Any(e => e.IdAlbaraVenda == id)).GetValueOrDefault();
        }

    }
}
