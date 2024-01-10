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
    public class PersonasController : Controller
    {
        private readonly PiuContext _context;

        public PersonasController(PiuContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            var piuContext = _context.Personas.Include(p => p.Usuario);
            return View(await piuContext.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            // Obtener el valor del usuario id desde la consulta de la URL
            
            var UsuarioId = HttpContext.Session.GetInt32("UsuarioId");

            // Verificar si el valor no está vacío y es un número válido
            if (UsuarioId.HasValue && UsuarioId.Value > 0)
            {
                // Obtener el usuario con el id especificado
                var usuario = _context.Usuarios.Find(UsuarioId.Value);

                // Verificar si el usuario existe
                if (usuario != null)
                {
                    // Crear una nueva persona con el usuario especificado
                    var persona = new Persona
                    {
                        UsuarioId = usuario.Id,
                        Usuario = usuario
                    };

                    // Enviar la persona a la vista
                    return View(persona);
                }
            }   
            else
            {
                // Manejar el caso donde el valor no es válido, por ejemplo, asignar un valor predeterminado o redirigir a otra página
                // ViewData["UsuarioId"] = valorPredeterminado;
            }

            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Rut,Nombre,ApellidoPaterno,ApellidoMaterno,Celular,Activo")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", persona.UsuarioId);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", persona.UsuarioId);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Rut,Nombre,ApellidoPaterno,ApellidoMaterno,Celular,Activo")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar si la persona existe
                    var existingPersona = await _context.Personas.FindAsync(id);

                    if (existingPersona == null)
                    {
                        // Si no existe, crear una nueva persona
                        _context.Add(persona);
                        await _context.SaveChangesAsync();

                        // Asignar el UsuarioId al nuevo usuario
                        existingPersona = persona;
                        existingPersona.UsuarioId = persona.Id;
                    }
                    else
                    {
                        // Actualizar la persona existente
                        _context.Update(persona);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
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

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", persona.UsuarioId);
            return View(persona);
        }


        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'PiuContext.Personas'  is null.");
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return (_context.Personas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
