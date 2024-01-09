using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PIU.Models;
using System.Security.Cryptography;

namespace PIU.Controllers
{
    public class LoginController : Controller
    {
        private readonly PiuContext _context;

        public LoginController(PiuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var usuarioAutenticado = await _context.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u =>
                        (u.Nombre == model.Nombre || u.Correo == model.Nombre) &&
                        u.Activo == true);

                if (usuarioAutenticado != null && VerifyPassword(model.Contrasena, usuarioAutenticado.Contrasena, usuarioAutenticado.Salt))
                {
                    // Autenticación exitosa, podrías almacenar información del usuario en la sesión o utilizar ASP.NET Core Identity.
                    HttpContext.Session.SetString("UserId", usuarioAutenticado.Id.ToString());
                    HttpContext.Session.SetString("UserName", usuarioAutenticado.Nombre);
                    HttpContext.Session.SetString("UserEmail", usuarioAutenticado.Correo);
                    HttpContext.Session.SetString("UserRol", usuarioAutenticado.Rol.Nombre);

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


        private bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(enteredPassword, Convert.FromBase64String(salt), 10000))
            {
                string enteredPasswordHash = Convert.ToBase64String(deriveBytes.GetBytes(32));
                return storedHash.Equals(enteredPasswordHash);
            }
        }
    }

}
