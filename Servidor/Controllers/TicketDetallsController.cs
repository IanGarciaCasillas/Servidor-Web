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
    [Route("ticketDetalls")]
    [ApiController]
    public class TicketDetallsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public TicketDetallsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/TicketDetalls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDetall>>> GetTicketDetalls()
        {
          if (_context.TicketDetalls == null)
          {
              return NotFound();
          }
            return await _context.TicketDetalls.ToListAsync();
        }

        // GET: api/TicketDetalls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDetall>> GetTicketDetall(int id)
        {
          if (_context.TicketDetalls == null)
          {
              return NotFound();
          }
            var ticketDetall = await _context.TicketDetalls.FindAsync(id);

            if (ticketDetall == null)
            {
                return NotFound();
            }

            return ticketDetall;
        }

        // PUT: api/TicketDetalls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketDetall(int id, TicketDetall ticketDetall)
        {
            if (id != ticketDetall.IdTicket)
            {
                return BadRequest();
            }

            _context.Entry(ticketDetall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketDetallExists(id))
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

        // POST: api/TicketDetalls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketDetall>> PostTicketDetall(TicketDetall ticketDetall)
        {
          if (_context.TicketDetalls == null)
          {
              return Problem("Entity set 'DbProjecteContext.TicketDetalls'  is null.");
          }
            _context.TicketDetalls.Add(ticketDetall);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketDetallExists(ticketDetall.IdTicket))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketDetall", new { id = ticketDetall.IdTicket }, ticketDetall);
        }

        // DELETE: api/TicketDetalls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketDetall(int id)
        {
            if (_context.TicketDetalls == null)
            {
                return NotFound();
            }
            var ticketDetall = await _context.TicketDetalls.FindAsync(id);
            if (ticketDetall == null)
            {
                return NotFound();
            }

            _context.TicketDetalls.Remove(ticketDetall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketDetallExists(int id)
        {
            var result =  (_context.TicketDetalls?.Any(e => e.IdTicket == id )).GetValueOrDefault();

            var algo = 1;
            return result;
        }


        //FUNCIONS PERSONALITZADES


        [HttpGet("{id}/{numDocument}")]
        public async Task<ActionResult<List<TicketDetall>>> GetTicketsDetalls(int id, int numDocument)
        {
            if (_context.TicketDetalls == null)
            {
                return NotFound();
            }
            var ticketsDetalls = await _context.TicketDetalls.ToListAsync();
            List<TicketDetall> detalls = new List<TicketDetall>();
            foreach (var detall in ticketsDetalls)
            {
                if (detall.IdTicket == id && detall.NumDocument == numDocument)
                    detalls.Add(detall);
            }

            return detalls;
        }

    }
}
