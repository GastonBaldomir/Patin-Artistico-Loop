using Loop_Patin.Data;
using Loop_Patin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class AlumnaController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly LoopDbContext _context;
    public AlumnaController(UserManager<Usuario> userManager , LoopDbContext context)
    {
        _userManager = userManager;
            _context = context;
    }
   
    public async Task<IActionResult> Index()
    {
        var alumnas = await _userManager.GetUsersInRoleAsync("Patinadora");
        var categorias = _context.Categorias.ToList();

        ViewBag.Categorias = categorias;

        return View(alumnas);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AsignarCategoria(string usuarioId, int? categoriaId)
    {
        var user = await _userManager.FindByIdAsync(usuarioId);
        if (user == null) return NotFound();

        user.CategoriaId = categoriaId;

        await _userManager.UpdateAsync(user);

        return RedirectToAction(nameof(Index));
    }
}
