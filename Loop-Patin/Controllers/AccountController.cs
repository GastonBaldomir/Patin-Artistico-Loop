using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Loop_Patin.Models;

namespace Loop_Patin.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AccountController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // GET: /Account/Login
        public IActionResult Login()
        {

            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                RolesDisponibles = new List<string>
        {
            "Padre/Tutor",
            "Patinadora"
        }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // IMPORTANTE: volver a cargar los roles
                model.RolesDisponibles = new List<string>
                {
                    "Padre/Tutor",
                    "Patinadora"
                };

                return View(model);
            }

            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                FechaRegistro = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Asignar rol elegido
                await _userManager.AddToRoleAsync(user, model.RolSeleccionado);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // Si falla la creación, mostrar errores
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Volver a cargar roles antes de devolver la vista
            model.RolesDisponibles = new List<string>
            {
                "Padre/Tutor",
            "Patinadora"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
