using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack.Formatters;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("comandaVenda")]
    [ApiController]
    public class ComandaVendumsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public ComandaVendumsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/ComandaVendums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComandaVendum>>> GetComandaVenda()
        {
          if (_context.ComandaVenda == null)
          {
              return NotFound();
          }
            return await _context.ComandaVenda.ToListAsync();
        }

        // GET: api/ComandaVendums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaVendum>> GetComandaVendum(int id)
        {
          if (_context.ComandaVenda == null)
          {
              return NotFound();
          }
            var comandaVendum = await _context.ComandaVenda.FindAsync(id);

            if (comandaVendum == null)
            {
                return NotFound();
            }

            return comandaVendum;
        }

        // PUT: api/ComandaVendums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComandaVendum(int id, ComandaVendum comandaVendum)
        {
            if (id != comandaVendum.IdComanda)
            {
                return BadRequest();
            }

            _context.Entry(comandaVendum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaVendumExists(id))
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

        // POST: api/ComandaVendums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComandaVendum>> PostComandaVendum(ComandaVendum comandaVendum)
        {
          if (_context.ComandaVenda == null)
          {
              return Problem("Entity set 'DbProjecteContext.ComandaVenda'  is null.");
          }
            _context.ComandaVenda.Add(comandaVendum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComandaVendum", new { id = comandaVendum.IdComanda }, comandaVendum);
        }

        // DELETE: api/ComandaVendums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComandaVendum(int id)
        {
            if (_context.ComandaVenda == null)
            {
                return NotFound();
            }
            var comandaVendum = await _context.ComandaVenda.FindAsync(id);
            if (comandaVendum == null)
            {
                return NotFound();
            }

            _context.ComandaVenda.Remove(comandaVendum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComandaVendumExists(int id)
        {
            return (_context.ComandaVenda?.Any(e => e.IdComanda == id)).GetValueOrDefault();
        }


        //FUNCIONS PERSONALITZADES

        



        [HttpGet("comandesClient")]
        public async Task<IActionResult>ComandesClient(int idClient)
        {
            var llistaComandesClient = await _context.ComandaVenda.ToListAsync<ComandaVendum>();

            var llistaFinal = llistaComandesClient.Where(order => order.IdClient == idClient).ToList();

            
            return Ok(new {status = 200,comandes = llistaFinal});
        }


        [HttpPost("newComanda")]
        public async Task<IActionResult> NewComandaVenda(List<ComandaVendaDetall> llistaArticles, int idClient)
        {
            int lastIdComandaVenda = LastComandaVenda();

            ComandaVendum newComandaVenda = new ComandaVendum();
            newComandaVenda.IdComanda = lastIdComandaVenda;
            newComandaVenda.DataComanda = DateTime.Now;
            newComandaVenda.EstatComandaVenda = EstatComandaVenda.Pendent.ToString();
            newComandaVenda.IdClient = idClient;

            _context.ComandaVenda.Add(newComandaVenda);

            for (int i = 0; i < llistaArticles.Count; i++)
            {
                ComandaVendaDetall detall = new ComandaVendaDetall();
                detall.IdComandaVenda = lastIdComandaVenda;
                detall.IdArticle = llistaArticles[i].IdArticle;
                detall.QuantitatDemanada = llistaArticles[i].QuantitatDemanada;

                _context.ComandaVendaDetalls.Add(detall);
            }
            await _context.SaveChangesAsync();
            

            return Ok(new { StatusCode = 200 });
        }


        /// <summary>
        /// Funcio que retorna el proxim id que es pot utilizar en Comandes Vendes
        /// </summary>
        /// <returns>Retorna el proxim id que es pot utilizar en Comandes Vendes</returns>
        private int LastComandaVenda()
        {
            int lastIdComanda = -1;
            var listComandaVenda = _context.ComandaVenda.ToList<ComandaVendum>();
            
            if(listComandaVenda.Count>0)
            {
                var lastComanda = listComandaVenda.Last();
                lastIdComanda = lastComanda.IdComanda;
            }
            else
            {
                lastIdComanda = 0;
            }
            lastIdComanda++;

            return lastIdComanda;
        }
        

    }
}
