using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public TicketsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.IdTicket)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'DbProjecteContext.Tickets'  is null.");
          }
            _context.Tickets.Add(ticket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketExists(ticket.IdTicket))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicket", new { id = ticket.IdTicket }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Tickets?.Any(e => e.IdTicket == id)).GetValueOrDefault();
        }


        //FUNCIONS PERSONALITZADES

        [HttpPost("LliurarFactura")]
        public async Task<IActionResult> LliurarFacturaClient(int numTicket, int numDocument, Client client)
        {
            var ticketClient = await TicketGet(numTicket, numDocument);

            if (ticketClient.Value != null)
            {
                if (ticketClient.Value.IdClient != null)
                    return Ok(new { status = 301 });
                else
                {                
                    ticketClient.Value.IdClient = client.IdClient;

                    _context.Entry(ticketClient.Value).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                        //EnviarTicketCorreu(ticketClient.Value, client);
                        return Ok(new { status = 200 });
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return BadRequest();
                    }
                }
            }
            else
            {
                return Ok(new { status = 300 });
            }


        }

        private void EnviarTicketCorreu(Ticket ticket, Client client)
        {

            DocumentFactura document = new DocumentFactura(_context);

            var doc = document.ExportPdf(ticket, client);
        }

        [HttpGet("GetTicketClient")]
        public async Task<IActionResult> GetTicketClient(int idClient)
        {
            var llistaTickets = await _context.Tickets.ToListAsync();

            var llistaTicketsClient = llistaTickets.Where(ticket=> ticket.IdClient == idClient).ToList();

            if (llistaTicketsClient.Count > 0)
                return Ok(new { status = 200, tickets = llistaTicketsClient });
            
            else
                return Ok(new { status = 201 });
        }


        [HttpPost("{NumDocument}")]
        public async Task<IActionResult> GetLastTicketAny(int NumDocument)
        {
            if(_context.Tickets.Any(ticket =>ticket.NumDocument == NumDocument))
            {
                List<Ticket> llistaAny = await _context.Tickets.Where(ticket => ticket.NumDocument == NumDocument).ToListAsync<Ticket>();
                var ultimTicket= llistaAny.Last<Ticket>();
                return Ok(ultimTicket.IdTicket);
            }
            else
            {
                //No hi ha cap ticket d'aquest ANY (PRIMER TICKET ANY)
                return Ok(0);
            }
        }


        [HttpGet("{id}/{numDocument}")]
        public async Task<ActionResult<Ticket>> TicketGet(int id, int numDocument)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id, numDocument);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }


    }
}
