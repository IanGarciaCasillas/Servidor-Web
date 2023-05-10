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
    [Route("client")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public ClientsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.IdClient)
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (_context.Clients == null)
            {
                return Problem("Client null");
            }
            try
            {
                bool correuClon = BuscarClientCorreu(client.CorreuClient);
                if(!correuClon)
                {
                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, newClient = new { client.IdClient, client.Dni, client.NomClient, client.Cognom1Client,
                        client.Cognom2Client, client.CorreuClient, client.ContrasenyaClient,
                        client.TelefonClient, client.DireccioClient, client.CodicPostal, client.Token } });
                }
                else
                {
                    return Ok(new { status = 201, problema = "Correu existent" });
                }

            }
            catch(DbUpdateException ex)
            {
                return Problem("No es pot inserir el client");
            }
            

            return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
        }

        private bool BuscarClientCorreu(string correuClient)
        {
            bool result = false;

            var llistaClients = _context.Clients.ToListAsync();
            bool trobat = false;
            int idx = 0;
            while(!trobat && idx < llistaClients.Result.Count)
            {
                var clientSelect = llistaClients.Result[idx];
                if (clientSelect.CorreuClient == correuClient)
                    trobat = true;
                idx++;
            }
            if (trobat)
                result = true;

            return result;
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.IdClient == id)).GetValueOrDefault();
        }

        //FUNCIONS PERSONALITZADES

        //FUNCION FUNCIONA AMB EL TESTING
        [HttpGet("login")]
        public async Task<IActionResult> LoginClient(string correuClient, string passwordClient)
        {
            var nada = 1;
            Client clientSelect = new Client();

            var listClients = await _context.Clients.ToListAsync<Client>();
            int idx = 0;
            bool clientTrobat = false;
            bool credencialError = false;
            while (!clientTrobat && !credencialError && idx < listClients.Count)
            {
                clientSelect = listClients[idx];
                idx++;
                if (clientSelect.CorreuClient == correuClient && clientSelect.ContrasenyaClient != passwordClient)
                    credencialError = true;
                if (clientSelect.CorreuClient == correuClient && clientSelect.ContrasenyaClient == passwordClient)
                    clientTrobat = true;
            }
            if (clientTrobat)
                return Ok(new { status = "Registrat", client = clientSelect });
            if (credencialError)
                return Ok(new { status = "InCorrect" });
            else
                return Ok(new { status = "SenseRegistre" });
        }

        //FUNCIO NOTIFICACIO
        [HttpPost("{titol}/{body}/{idClient}")]
        public async Task<IActionResult> NotifcacioClient(string titol, string body, int idClient)
        {
            MobilNotific mobil = new MobilNotific();
            Client clientSelect = await _context.Clients.FindAsync(idClient);
            var result = mobil.SendNotific(titol, body, clientSelect.Token);
            var espera = 1;
            return Ok(result);
        }
    }
}
