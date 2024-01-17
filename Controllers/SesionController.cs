using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PIU.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

namespace PIU.Controllers
{
    public class SesionController : Controller
    {
        private readonly PiuContext _context;

        public SesionController(PiuContext piuContext)
        {
            _context = piuContext;
        }

        [HttpGet]
        public IActionResult Ingresar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ingresar(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u =>
                        (u.Nombre == model.Nombre || u.Correo == model.Nombre) &&
                        u.Activo == true);

                if (usuario != null && VerificarContrasena(model.Contrasena, usuario.Contrasena, usuario.Salt))
                {
                    // Establecer la sesión del usuario como iniciada
                    var persona = _context.Personas
                        .Include(p => p.Usuario)
                        .FirstOrDefault(p => p.UsuarioId == usuario.Id);
                    HttpContext.Session.Set("Sesion", BitConverter.GetBytes(true));
                    HttpContext.Session.Set("IdUsuario", Encoding.UTF8.GetBytes(usuario.Id.ToString()));
                    if (usuario.Nombre != null)
                    {
                        byte[] nombreBytes = Encoding.UTF8.GetBytes(usuario.Nombre);
                        HttpContext.Session.Set("NombreUsuario", nombreBytes);
                    }
                    if (usuario.Rol?.Nombre != null)
                    {
                        byte[] nombreBytes = Encoding.UTF8.GetBytes(usuario.Rol.Nombre);
                        HttpContext.Session.Set("RolUsuario", nombreBytes);
                    }

                    if (persona?.Nombre != null)
                    {
                        byte[] nombreBytes = Encoding.UTF8.GetBytes(persona.Nombre);
                        HttpContext.Session.Set("NombrePersona", nombreBytes);
                    }
                    if (persona?.ApellidoPaterno != null)
                    {
                        byte[] apellidoBytes = Encoding.UTF8.GetBytes(persona.ApellidoPaterno);
                        HttpContext.Session.Set("ApellidoPaternoPersona", apellidoBytes);
                    }
                    if (persona?.ApellidoMaterno != null)
                    {
                        byte[] apellidoBytes = Encoding.UTF8.GetBytes(persona.ApellidoMaterno);
                        HttpContext.Session.Set("ApellidoMaternoPersona", apellidoBytes);
                    }
                    TempData["SuccessMessage"] = "Sesión iniciada correctamente";
                    return RedirectToAction("Index", "Home");
                }
                else if (usuario != null)
                {
                    TempData["ErrorMessage"] = "Credenciales inválidas";
                    return RedirectToAction("Ingresar", "Sesion");
                }
                else
                {
                    TempData["ErrorMessage"] = "Su usuario actualmente se encuentra inhabilitado, contacte con el administrador si considera que se trata de un error";
                    return RedirectToAction("Ingresar", "Sesion");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            // Agregar mensaje de "Has cerrado sesión"
            TempData["SuccessMessage"] = "Has cerrado sesión";
            // Redirecciona al usuario a la página de inicio de sesión después de cerrar sesión
            return RedirectToAction("Index", "Home");
        }
        public static class PasswordHelper
        {
            private const int SaltSize = 32;
            private const int Iterations = 10000;

            public static (string Hash, string Salt) HashPassword(string password)
            {
                using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
                {
                    byte[] salt = deriveBytes.Salt;
                    string hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
                    return (hash, Convert.ToBase64String(salt));
                }
            }
        }
        private bool VerificarContrasena(string contrasena, string hashedPassword, string salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(contrasena, Convert.FromBase64String(salt), 10000))
            {
                byte[] saltBytes = deriveBytes.Salt;
                string hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
                return hash == hashedPassword;
            }
        }
    }
}

