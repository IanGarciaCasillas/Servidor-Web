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
    [Route("comandaVendaDetalls")]
    [ApiController]
    public class ComandaVendaDetallsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public ComandaVendaDetallsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/ComandaVendaDetalls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComandaVendaDetall>>> GetComandaVendaDetalls()
        {
            if (_context.ComandaVendaDetalls == null)
            {
                return NotFound();
            }
            return await _context.ComandaVendaDetalls.ToListAsync();
        }

        // GET: api/ComandaVendaDetalls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaVendaDetall>> GetComandaVendaDetall(int id)
        {
            if (_context.ComandaVendaDetalls == null)
            {
                return NotFound();
            }
            var comandaVendaDetall = await _context.ComandaVendaDetalls.FindAsync(id);

            if (comandaVendaDetall == null)
            {
                return NotFound();
            }

            return comandaVendaDetall;
        }

        // PUT: api/ComandaVendaDetalls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComandaVendaDetall(int id, ComandaVendaDetall comandaVendaDetall)
        {
            if (id != comandaVendaDetall.IdComandaVenda)
            {
                return BadRequest();
            }

            _context.Entry(comandaVendaDetall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaVendaDetallExists(id))
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

        // POST: api/ComandaVendaDetalls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComandaVendaDetall>> PostComandaVendaDetall(ComandaVendaDetall comandaVendaDetall)
        {
            if (_context.ComandaVendaDetalls == null)
            {
                return Problem("Entity set 'DbProjecteContext.ComandaVendaDetalls'  is null.");
            }
            _context.ComandaVendaDetalls.Add(comandaVendaDetall);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComandaVendaDetallExists(comandaVendaDetall.IdComandaVenda))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetComandaVendaDetall", new { id = comandaVendaDetall.IdComandaVenda }, comandaVendaDetall);
        }

        // DELETE: api/ComandaVendaDetalls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComandaVendaDetall(int id)
        {
            if (_context.ComandaVendaDetalls == null)
            {
                return NotFound();
            }
            var comandaVendaDetall = await _context.ComandaVendaDetalls.FindAsync(id);
            if (comandaVendaDetall == null)
            {
                return NotFound();
            }

            _context.ComandaVendaDetalls.Remove(comandaVendaDetall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComandaVendaDetallExists(int id)
        {
            return (_context.ComandaVendaDetalls?.Any(e => e.IdComandaVenda == id)).GetValueOrDefault();
        }



        //FUNCIONS PERSONALITZADES
        [HttpGet("GetDetalls/{idComanda}")]
        public async Task<ActionResult<List<ComandaVendaDetall>>> GetDetallsTPV(int idComanda)
        {
            var comandesDetalls = await _context.ComandaVendaDetalls.ToListAsync<ComandaVendaDetall>();

            var llistaFinal = comandesDetalls.Where(detall => detall.IdComandaVenda == idComanda).ToList();


            return llistaFinal;
        }


        [HttpGet("GetDetalls")]
        public async Task<IActionResult> GetDetalls(int idComanda)
        {
            var comandesDetalls = await _context.ComandaVendaDetalls.ToListAsync<ComandaVendaDetall>();

            var llistaFinal = comandesDetalls.Where(detall => detall.IdComandaVenda == idComanda).ToList();


            return Ok(new { detalls = llistaFinal });
        }



        [HttpPost("GetArticlesDetalls")]
        public async Task<IActionResult> GetArticlesDetalls(ComandaVendum comanda)
        {
            var comandesDetalls = await _context.ComandaVendaDetalls.ToListAsync<ComandaVendaDetall>();

            var llistaFinal = comandesDetalls.Where(detall => detall.IdComandaVenda == comanda.IdComanda).ToList();



            List<Article> llistaArticles = new List<Article>();

            var articles = await _context.Articles.ToListAsync<Article>();
            var idArticles = llistaFinal.Select(detall => detall.IdArticle).ToList();

            //var articles = _context.Articles.Where(article => idArticles.Contains(article.IdArticle)).ToList<Article>();
            var articlesList = articles.Where(article => idArticles.Contains(article.IdArticle)).ToList();



            return Ok(new { listArticles = articles });
        }

        [HttpPut("UpdateComandaVendaDetall/{idComanda}/{idArticle}")]
        public async Task<IActionResult> UpdateComandaVendaDetall(int idComanda, int idArticle, ComandaVendaDetall comandaVendaDetall)
        {

            if (idComanda != comandaVendaDetall.IdComandaVenda)
            {
                return BadRequest();
            }

            _context.Entry(comandaVendaDetall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaVendaDetallExists(idComanda))
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
    }
}
