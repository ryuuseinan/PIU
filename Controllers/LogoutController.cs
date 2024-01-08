using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIU.Models;

namespace PIU.Controllers
{
    public class LogoutController : Controller
    {
        private readonly PiuContext _context;

        public LogoutController(PiuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Usuario model)
        {
            if (ModelState.IsValid)
            {
                // Aquí deberías implementar la lógica de autenticación.
                // Verifica las credenciales en la base de datos u otro sistema de autenticación.

                var usuarioAutenticado = await _context.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u => u.Nombre == model.Nombre || u.Nombre == model.Correo && u.Contrasena == model.Contrasena);

                if (usuarioAutenticado != null)
                {
                    // Autenticación exitosa, podrías almacenar información del usuario en la sesión o utilizar ASP.NET Core Identity.
                    // Por ejemplo:
                    // HttpContext.Session.SetString("UserId", usuarioAutenticado.Id.ToString());
                    HttpContext.Session.SetString("UserId", usuarioAutenticado.Id.ToString());
                    HttpContext.Session.SetString("UserName", usuarioAutenticado.Nombre);
                    HttpContext.Session.SetString("UserEmail", usuarioAutenticado.Correo);
                    //HttpContext.Session.SetString("UserRol", usuarioAutenticado.Rol.Nombre);

                    ModelState.AddModelError(string.Empty, "Ingreso exitoso.");
                    return RedirectToAction("Index", "Home"); // Redirige a la página principal después de iniciar sesión.
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Credenciales inválidas o cuenta desactivada.");
                }
            }

            return View(model);
        }
    }
}
