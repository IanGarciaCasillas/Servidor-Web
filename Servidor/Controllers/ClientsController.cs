using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using Servidor.Model;

namespace Servidor.Controllers
{
    [Route("/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DBContext _context;

        public ClientController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
          if (_context.Client == null)
          {
              return NotFound();
          }
            return await _context.Client.ToListAsync();
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
          if (_context.Client == null)
          {
              return NotFound();
          }
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
          if (_context.Client == null)
          {
              return Problem("Entity set 'DBContext.Client'  is null.");
          }
            _context.Client.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        ////FUNCIONS PERSONALITZADES


        [HttpGet("login")]
        public async Task<IActionResult> LoginClient(string correuClient, string passwordClient)
        {
            var nada = 1;
            Client clientSelect = new Client();

            var listClients = await _context.Client.ToListAsync<Client>();
            int idx = 0;
            bool clientTrobat = false;
            bool credencialError = false;
            while(!clientTrobat && !credencialError && idx<listClients.Count)
            {
                clientSelect = listClients[idx];
                idx++;
                if (clientSelect.Correu == correuClient && clientSelect.Contrasenya != passwordClient)
                    credencialError = true;
                if (clientSelect.Correu == correuClient && clientSelect.Contrasenya == passwordClient)
                    clientTrobat = true;
            }
            if (clientTrobat)
                return Ok(new { status = "Registrat", client = clientSelect });
            if(credencialError)
                return Ok(new { status = "InCorrect" });
            else
                return Ok(new { status = "SenseRegistre" });
        }
    }
}
