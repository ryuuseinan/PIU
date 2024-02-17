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
    public class SesionsController : Controller
    {
        private readonly PiuContext _context;

        public SesionsController(PiuContext context)
        {
            _context = context;
        }

        // GET: Sesions
        public async Task<IActionResult> Index()
        {
            var piuContext = _context.Sesions.Include(s => s.Estudiante);
            return View(await piuContext.ToListAsync());
        }

        // GET: Sesions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sesions == null)
            {
                return NotFound();
            }

            var sesion = await _context.Sesions
                .Include(s => s.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesion == null)
            {
                return NotFound();
            }

            return View(sesion);
        }

        // GET: Sesions/Create
        public IActionResult Create()
        {
            // Recuperar el EstudianteId de la sesión
            var EstudianteId = HttpContext.Session.GetInt32("EstudianteId");
            var EstudianteNombre = HttpContext.Session.GetString("EstudianteNombre");
            // Verificar si el EstudianteId es nulo
            if (EstudianteId.HasValue)
            {
                // Hacer lo que necesites con el EstudianteId, por ejemplo, pasarle a la vista si es necesario
                ViewBag.EstudianteId = EstudianteId.Value;
                // pasar el valor EstudianteNombre a la vista
                ViewBag.EstudianteNombre = EstudianteNombre;

                // También podrías pasar el EstudianteId a través del modelo si lo prefieres
                // var model = new TuModelo();
                // model.EstudianteId = EstudianteId.Value;
                // return View(model);

                // Luego, retornar la vista
                return View();
            }
            else
            {
                // Si no se encuentra el EstudianteId en la sesión, podrías redirigir a una página de error o tomar otra acción apropiada.
                // Por ejemplo:
                // return RedirectToAction("Error", "Home");
                // O simplemente retornar una vista con un mensaje indicando que no se encontró el EstudianteId en la sesión.
                return View("Error", "No se encontró el estudiante en la sesión.");
            }
        }

        // POST: Sesions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstudianteId,FechaInicio,FechaTermino,ViaContacto,Objetivo,ObservacionInicio,ObservacionDesarrollo,ObservacionCierre,AccionInicio,AccionDesarrollo,AccionCierre,Asistio")] Sesion sesion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sesion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", sesion.EstudianteId);
            return View(sesion);
        }

        // GET: Sesions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sesions == null)
            {
                return NotFound();
            }

            var sesion = await _context.Sesions.FindAsync(id);
            if (sesion == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", sesion.EstudianteId);
            return View(sesion);
        }

        // POST: Sesions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstudianteId,FechaInicio,FechaTermino,ViaContacto,Objetivo,ObservacionInicio,ObservacionDesarrollo,ObservacionCierre,AccionInicio,AccionDesarrollo,AccionCierre,Asistio")] Sesion sesion)
        {
            if (id != sesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Actualizar el modelo con los datos del formulario
                await TryUpdateModelAsync(sesion);

                // Verificar el valor del checkbox de asistencia
                try
                {
                    _context.Update(sesion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Sesions", new { id = sesion.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesionExists(sesion.Id))
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
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", sesion.EstudianteId);
            return View(sesion);
        }

        // GET: Sesions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sesions == null)
            {
                return NotFound();
            }

            var sesion = await _context.Sesions
                .Include(s => s.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesion == null)
            {
                return NotFound();
            }

            return View(sesion);
        }

        // POST: Sesions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sesions == null)
            {
                return Problem("Entity set 'PiuContext.Sesions'  is null.");
            }
            var sesion = await _context.Sesions.FindAsync(id);
            if (sesion != null)
            {
                _context.Sesions.Remove(sesion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesionExists(int id)
        {
          return (_context.Sesions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
