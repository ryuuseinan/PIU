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
    public class PerfilController : Controller
    {
        private readonly PiuContext _context;

        public PerfilController(PiuContext context)
        {
            _context = context;
        }

        // GET: Perfil
        // GET: Perfil
        public async Task<IActionResult> Index(int? userId)
        {
            if (userId == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.UsuarioId == userId);

            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Perfil/Edit/5
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
        private bool PersonaExists(int id)
        {
          return (_context.Personas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
