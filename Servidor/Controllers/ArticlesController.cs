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
    [Route("articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public ArticlesController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
          if (_context.Articles == null)
          {
              return NotFound();
          }
            return await _context.Articles.ToListAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
          if (_context.Articles == null)
          {
              return NotFound();
          }
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.IdArticle)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { status = 200 });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
          if (_context.Articles == null)
          {
              return Problem("Entity set 'DbProjecteContext.Articles'  is null.");
          }
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.IdArticle }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (_context.Articles == null)
            {
                return NotFound();
            }
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return (_context.Articles?.Any(e => e.IdArticle == id)).GetValueOrDefault();
        }

        //FUNCIONS PERSONALITZADES

        [HttpGet("BuscarArticle")]
        public async Task<ActionResult<IEnumerable<Article>>> BuscarArticle(string nomArticle)
        {
                        
            var llistaArticle = await _context.Articles.ToListAsync();

            List<Article> llistaFiltrada = new List<Article>();

            foreach(var article in llistaArticle)
            {
                if (article.NomArticle.ToLower().Contains(nomArticle.ToLower()))
                    llistaFiltrada.Add(article);
            }
            if (llistaFiltrada.Count >= 1)
                return Ok(new { status = 200, llista = llistaFiltrada });
            else
                return Ok(new { status = 201 });
        }



        [HttpPost("GetArticlesDetalls")]
        public async Task<IActionResult> GetArticlesDetalls(List<int> llista)
        {
            List<Article> llistaArticles = new List<Article>();

            var articles = await _context.Articles.ToListAsync<Article>();
            var idArticles = llista;

            var articlesList = articles.Where(article => idArticles.Contains(article.IdArticle)).ToList();



            return Ok(new { listArticles = articlesList });
        }

    }
}
