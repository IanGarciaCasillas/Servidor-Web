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
    [Route("empl")]
    [ApiController]
    public class EmplsController : ControllerBase
    {
        private readonly DbProjecteContext _context;

        public EmplsController(DbProjecteContext context)
        {
            _context = context;
        }

        // GET: api/Empls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empl>>> GetEmpls()
        {
          if (_context.Empls == null)
          {
              return NotFound();
          }
            return await _context.Empls.ToListAsync();
        }

        // GET: api/Empls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empl>> GetEmpl(int id)
        {
          if (_context.Empls == null)
          {
              return NotFound();
          }
            var empl = await _context.Empls.FindAsync(id);

            if (empl == null)
            {
                return NotFound();
            }

            return empl;
        }

        // PUT: api/Empls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpl(int id, Empl empl)
        {
            if (id != empl.IdEmpl)
            {
                return BadRequest();
            }

            _context.Entry(empl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmplExists(id))
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

        // POST: api/Empls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empl>> PostEmpl(Empl empl)
        {
          if (_context.Empls == null)
          {
              return Problem("Entity set 'DbProjecteContext.Empls'  is null.");
          }
            _context.Empls.Add(empl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpl", new { id = empl.IdEmpl }, empl);
        }

        // DELETE: api/Empls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpl(int id)
        {
            if (_context.Empls == null)
            {
                return NotFound();
            }
            var empl = await _context.Empls.FindAsync(id);
            if (empl == null)
            {
                return NotFound();
            }

            _context.Empls.Remove(empl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmplExists(int id)
        {
            return (_context.Empls?.Any(e => e.IdEmpl == id)).GetValueOrDefault();
        }




        [HttpPost("{userEmpl}/{passwordEmpl}")]
        public async Task<IActionResult> LoginEmpl(string userEmpl, string passwordEmpl)
        {
            Empl emplSelect = new Empl();

            var listEmpl = await _context.Empls.ToListAsync<Empl>();
            int idx = 0;
            bool emplTrobat = false;
            
            while (!emplTrobat && idx < listEmpl.Count)
            {
                emplSelect= listEmpl[idx];
                idx++;
                if(emplSelect.UserEmpl == userEmpl && emplSelect.PasswordEmpl == passwordEmpl)
                    emplTrobat= true;
            }
            if (emplTrobat)
                return Ok(new { status = 200, empl = emplSelect });
            else
                return Ok(new { status = 400 });
        }

    }
}
