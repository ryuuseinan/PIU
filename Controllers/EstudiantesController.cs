using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PIU.Models;

namespace PIU.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly PiuContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EstudiantesController(PiuContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var piuContext = _context.Estudiantes
                .Include(e => e.Campus)
                .Include(e => e.Carrera)
                .Include(e => e.Jornada);
            return View(await piuContext.ToListAsync());
        }

        // POST: Estudiantes/Search
        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return RedirectToAction(nameof(Index));
            }

            // Realiza la búsqueda en el contexto de la base de datos
            var estudiantes = await _context.Estudiantes
                .Where(e => (e.Rut.Contains(searchString) ||
                             e.Nombre.Contains(searchString) ||
                             e.ApellidoPaterno.Contains(searchString) ||
                             e.ApellidoMaterno.Contains(searchString)))
                .Include(e => e.Campus)
                .Include(e => e.Carrera)
                .Include(e => e.Jornada)
                .ToListAsync();

            // Pasa los resultados a la vista
            return View("Index", estudiantes);
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
                .Include(e => e.Jornada)
                .Include(e => e.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            string? fechaNacimiento = estudiante.FechaNacimiento?.ToShortDateString();
            ViewData["FechaNacimiento"] = fechaNacimiento;

            int edad = CalcularEdad(estudiante.FechaNacimiento ?? DateTime.MinValue);
            ViewData["Edad"] = edad;

            return View(estudiante);
        }
        // GET: Estudiantes/Sesion/5
        public async Task<IActionResult> Sesion(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Campus)
                .Include(e => e.Carrera)
                .Include(e => e.Jornada)
                .Include(e => e.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            string? fechaNacimiento = estudiante.FechaNacimiento?.ToShortDateString();
            ViewData["FechaNacimiento"] = fechaNacimiento;

            int edad = CalcularEdad(estudiante.FechaNacimiento ?? DateTime.MinValue);
            ViewData["Edad"] = edad;

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Id");
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Id");
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Id");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                //Asigna activo al estudiante
                estudiante.Activo = true;
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Nombre", estudiante.CampusId);
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", estudiante.CarreraId);
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Nombre", estudiante.JornadaId);
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

            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Nombre", estudiante.CampusId);
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", estudiante.CarreraId);

            // Lista de años (ajústala según tus necesidades)
            int currentYear = DateTime.Now.Year;
            List<int> anios = Enumerable.Range(2016, currentYear - 2016 + 1).ToList();

            ViewData["Anios"] = new SelectList(anios, estudiante.IngresoPiu);
            ViewData["Anios"] = new SelectList(anios, estudiante.EgresoPiu);
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Nombre", estudiante.JornadaId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", estudiante.GeneroId);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar los datos en la base de datos
                    var fechaNacimiento = estudiante.FechaNacimiento;
                    var fechaLimite = DateTime.Today.AddYears(-15);
                    if (fechaNacimiento > fechaLimite)
                    {
                        ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento debe ser anterior a 15 años de la fecha actual.");
                        // Recarga de los datos necesarios para el dropdown de la vista, en caso de necesitarlo
                        ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Nombre", estudiante.CampusId);
                        ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", estudiante.CarreraId);

                        // Lista de años (ajústala según tus necesidades)
                        int currentYear = DateTime.Now.Year;
                        List<int> anios = Enumerable.Range(2016, currentYear - 2016 + 1).ToList();

                        ViewData["Anios"] = new SelectList(anios, estudiante.IngresoPiu);
                        ViewData["Anios"] = new SelectList(anios, estudiante.EgresoPiu);
                        ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Nombre", estudiante.JornadaId);
                        ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", estudiante.GeneroId);
                        return View(estudiante);
                    }
                    estudiante.CorreoInstitucional += Request.Form["dominioSelector"];
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Estudiantes", new { id = estudiante.Id });
                }
                catch (Exception ex)
                {
                    // Manejar errores aquí...
                    ViewBag.Message = "Error: " + ex.Message;
                    return View(estudiante);
                }
            }

            // Si el modelo no es válido, regresa a la vista con el modelo
            ViewData["CampusId"] = new SelectList(_context.Campuses, "Id", "Nombre", estudiante.CampusId);
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", estudiante.CarreraId);
            ViewData["JornadaId"] = new SelectList(_context.Jornada, "Id", "Nombre", estudiante.JornadaId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", estudiante.GeneroId);
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
                estudiante.Activo = false;
                _context.Estudiantes.Update(estudiante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Modifica la ruta en la función ActualizarImagen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarImagen(int id, IFormFile FotoArchivo)
        {
            // Obtener el estudiante de la base de datos
            var estudiante = _context.Estudiantes.Find(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            var consoleMessages = new List<string>(); // Lista para almacenar mensajes de la consola

            try
            {
                // Lógica para manejar la carga de imágenes
                    if (FotoArchivo != null && FotoArchivo.Length > 0)
                    {
                        //Imprimir nombre de imagen
                        string fileName = FotoArchivo.FileName;
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "img", "Estudiante", "FotoPerfil");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string fileExtension = Path.GetExtension(fileName);
                        string uniqueFileName = $"{estudiante.Id}{fileExtension}";
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            // Guardar la imagen original
                            FotoArchivo.CopyTo(fileStream);
                        }

                        // Convertir la imagen a formato WebP
                        /*using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            using (var imageFactory = new ImageFactory(preserveExifData: true))
                        {
                                imageFactory.Load(FotoArchivo.OpenReadStream())
                                    .Format(new JpegFormat())
                                    .Quality(100)
                                    .Save(fileStream);
                            }
                        }*/
                    estudiante.Foto = "img/Estudiante/FotoPerfil/" + uniqueFileName; // Asignar el nombre único al modelo
                    }
                        
                _context.Update(estudiante);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "La imagen se ha actualizado correctamente.";
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción relacionada con la carga de imágenes
                // Puedes registrar errores, mostrar mensajes al usuario, etc.
                ViewBag.Message = "Error: " + ex.Message;
                return View(estudiante);
            }
            // Redireccionar al detalle del estudiante o a donde sea necesario
            return RedirectToAction("Details", "Estudiantes", new { id = estudiante.Id });
        }
        public int CalcularEdad(DateTime fechaNacimiento)
        {
            // Obtiene la fecha actual
            DateTime fechaActual = DateTime.Today;

            // Calcula la diferencia de años
            int edad = fechaActual.Year - fechaNacimiento.Year;

            // Verifica si la fecha de cumpleaños ya ocurrió en el año actual
            if (fechaNacimiento.Date > fechaActual.AddYears(-edad))
            {
                // Si no ha ocurrido, resta un año a la edad
                edad--;
            }

            return edad;
        }
        private bool EstudianteExists(int id)
        {
          return (_context.Estudiantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
