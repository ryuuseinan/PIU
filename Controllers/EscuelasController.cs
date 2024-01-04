using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIU.Models;

namespace PIU.Controllers
{
    public class EscuelasController : Controller
    {
        private readonly PiuContext _context;

        public EscuelasController(PiuContext context)
        {
            _context = context;
        }

        // GET: Escuelas
        public async Task<IActionResult> Index()
        {
            var piuContext = _context.Escuelas
                .Where(c => c.Activo == true);
            return View(await piuContext.ToListAsync());
        }

        // GET: Escuelas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Escuelas == null)
            {
                return NotFound();
            }

            var escuela = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escuela == null)
            {
                return NotFound();
            }

            return View(escuela);
        }

        // GET: Escuelas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Escuelas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Activo")] Escuela escuela)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escuela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escuela);
        }

        // GET: Escuelas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Escuelas == null)
            {
                return NotFound();
            }

            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }
            return View(escuela);
        }

        // POST: Escuelas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Activo")] Escuela escuela)
        {
            if (id != escuela.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escuela);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscuelaExists(escuela.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(escuela);
        }

        // GET: Escuelas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Escuelas == null)
            {
                return NotFound();
            }

            var escuela = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escuela == null)
            {
                return NotFound();
            }

            return View(escuela);
        }

        // POST: Escuelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Escuelas == null)
            {
                return Problem("Entity set 'PiuContext.Escuelas'  is null.");
            }
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela != null)
            {
                escuela.Activo = false;
                _context.Escuelas.Remove(escuela);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscuelaExists(int id)
        {
          return (_context.Escuelas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
