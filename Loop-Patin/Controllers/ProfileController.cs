using Loop_Patin.Models;
using Loop_Patin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<Usuario> _userManager;

    public ProfileController(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    // 🔹 GET: mostrar datos (preview)
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return NotFound();

        var model = new ProfileViewModel
        {
            Nombre = user.Nombre,
            Apellidos = user.Apellidos,
            Email = user.Email,
            FechaNacimiento = user.FechaNacimiento,
            Telefono = user.Telefono,
            Dni = user.Dni,
            Descripcion = user.Descripcion,
            FotoUrl = user.FotoUrl,
            FechaRegistro= user.FechaRegistro,
        };
        ViewBag.Success = true;
        return View(model);
    }

    // 🔹 POST: guardar cambios
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return NotFound();

        user.Nombre = model.Nombre;
        user.Apellidos = model.Apellidos;
        user.FechaNacimiento = model.FechaNacimiento;
        user.Telefono = model.Telefono;
        user.Dni = model.Dni;
        user.Descripcion = model.Descripcion;

        await _userManager.UpdateAsync(user);

        return View(model);
    }
    public async Task<IActionResult> Edit()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        var model = new ProfileViewModel
        {
            Nombre = user.Nombre,
            Apellidos = user.Apellidos,
            Email = user.Email,
            FechaNacimiento = user.FechaNacimiento,
            Telefono = user.Telefono,
            Dni = user.Dni,
            Descripcion = user.Descripcion,
            FotoUrl = user.FotoUrl,
        };

        return View(model);
    }

    // 🔹 EDITAR PERFIL (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        user.Nombre = model.Nombre;
        user.Apellidos = model.Apellidos;
        user.FechaNacimiento = model.FechaNacimiento;
        user.Telefono = model.Telefono;
        user.Dni = model.Dni;
        user.Descripcion = model.Descripcion;
        user.FotoUrl = model.FotoUrl;
        await _userManager.UpdateAsync(user);

        TempData["Success"] = "Perfil actualizado correctamente ✔️";
        TempData["Error"] = "❌ Algo salió mal";
        return RedirectToAction("Index", "Profile");

    }
}

