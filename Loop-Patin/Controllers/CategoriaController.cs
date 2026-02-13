using Microsoft.AspNetCore.Mvc;
using Loop_Patin.Data;
using Loop_Patin.Models;
using Loop_Patin.Models.ViewModels;

namespace Loop_Patin.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly LoopDbContext _context;

        public CategoriaController(LoopDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            var vm = new CategoriaIndexViewModel
            {
                Categorias = _context.Categorias.ToList()
            };

            return View(vm);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaIndexViewModel model)
        {
            Console.WriteLine($"Nombre: '{model.Nombre}'");
            Console.WriteLine($"Descripcion: '{model.Descripcion}'");
            if (!ModelState.IsValid)
            {
                // volvemos a cargar el listado
                var vm = new CategoriaIndexViewModel
                {
                    Categorias = _context.Categorias.ToList(),
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion
                };

                return View("Index", vm);
            }

            var categoria = new Categoria
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            TempData["Success"] = "Categoría creada correctamente";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria == null)
                return NotFound();

            var vm = new CategoriaIndexViewModel
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Categorias = _context.Categorias.ToList()
            };

            return View("Index", vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoriaIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categorias = _context.Categorias.ToList();
                return View("Index", model);
            }

            var categoria = _context.Categorias.Find(model.Id);

            if (categoria == null)
                return NotFound();

            categoria.Nombre = model.Nombre;
            categoria.Descripcion = model.Descripcion;

            _context.SaveChanges();

            TempData["Success"] = "Categoría modificada correctamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria == null)
                return NotFound();

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            TempData["Success"] = "Categoría Eliminada";
            return RedirectToAction(nameof(Index));
        }


      
    }

}
