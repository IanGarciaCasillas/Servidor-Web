﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servidor.Model;

namespace Servidor.Controllers
{
    [Route("client")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DBContext _context;

        public ClientsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
          if (_context.Client == null)
          {
              return NotFound();
          }
            return await _context.Client.ToListAsync();
        }

        // GET: api/Clients/5
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
          if (_context.Client == null)
          {
              return Problem("Entity set 'DBContext.Client'  is null.");
          }
            _context.Client.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
        }

        // DELETE: api/Clients/5
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
            return (_context.Client?.Any(e => e.IdClient == id)).GetValueOrDefault();
        }



        //FUNCIONS PERSONALITZADES

        //FUNCION FUNCIONA AMB EL TESTING
        [HttpGet("login")]
        public async Task<IActionResult> LoginClient(string correuClient, string passwordClient)
        {
            var nada = 1;
            Client clientSelect = new Client();

            var listClients = await _context.Client.ToListAsync<Client>();
            int idx = 0;
            bool clientTrobat = false;
            bool credencialError = false;
            while (!clientTrobat && !credencialError && idx < listClients.Count)
            {
                clientSelect = listClients[idx];
                idx++;
                if (clientSelect.CorreuClient == correuClient && clientSelect.ContrasenyaClient!= passwordClient)
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

    }
}
