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
    public class EstudiantesController : Controller
    {
        private readonly PiuContext _context;

        public EstudiantesController(PiuContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var piuContext = _context.Estudiantes.Include(e => e.Campus).Include(e => e.Carrera).Include(e => e.EgresoPiuNavigation).Include(e => e.Genero).Include(e => e.IngresoPiuNavigation).Include(e => e.Jornada);
            return View(await piuContext.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Campus)
                .Include(e => e.Carrera)
                .Include(e => e.EgresoPiuNavigation)
                .Include(e => e.Genero)
                .Include(e => e.IngresoPiuNavigation)
                .Include(e => e.Jornada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Id");
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Id");
            ViewData["EgresoPiu"] = new SelectList(_context.Anios, "Id", "Id");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id");
            ViewData["IngresoPiu"] = new SelectList(_context.Anios, "Id", "Id");
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Id");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rut,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Correo,Celular,IngresoPiu,EgresoPiu,CarreraId,CampusId,JornadaId,GeneroId,Foto,Activo")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Id", estudiante.CampusId);
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Id", estudiante.CarreraId);
            ViewData["EgresoPiu"] = new SelectList(_context.Anios, "Id", "Id", estudiante.EgresoPiu);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id", estudiante.GeneroId);
            ViewData["IngresoPiu"] = new SelectList(_context.Anios, "Id", "Id", estudiante.IngresoPiu);
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Id", estudiante.JornadaId);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Id", estudiante.CampusId);
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Id", estudiante.CarreraId);
            ViewData["EgresoPiu"] = new SelectList(_context.Anios, "Id", "Id", estudiante.EgresoPiu);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id", estudiante.GeneroId);
            ViewData["IngresoPiu"] = new SelectList(_context.Anios, "Id", "Id", estudiante.IngresoPiu);
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Id", estudiante.JornadaId);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rut,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Correo,Celular,IngresoPiu,EgresoPiu,CarreraId,CampusId,JornadaId,GeneroId,AsignaturaId,Foto,Activo")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
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
            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Id", estudiante.CampusId);
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Id", estudiante.CarreraId);
            ViewData["EgresoPiu"] = new SelectList(_context.Anios, "Id", "Id", estudiante.EgresoPiu);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id", estudiante.GeneroId);
            ViewData["IngresoPiu"] = new SelectList(_context.Anios, "Id", "Id", estudiante.IngresoPiu);
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Id", estudiante.JornadaId);
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Campus)
                .Include(e => e.Carrera)
                .Include(e => e.EgresoPiuNavigation)
                .Include(e => e.Genero)
                .Include(e => e.IngresoPiuNavigation)
                .Include(e => e.Jornada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estudiantes == null)
            {
                return Problem("Entity set 'PiuContext.Estudiantes'  is null.");
            }
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
          return (_context.Estudiantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
