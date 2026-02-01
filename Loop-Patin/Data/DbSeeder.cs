using Microsoft.AspNetCore.Identity;

namespace Loop_Patin.Data
{
    public static class DbSeeder // Clase estática
    {
        // Método asíncrono para la carga de datos
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Obtener los servicios necesarios (UserManager y RoleManager)
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            // 1. Crear los roles si no existen
            string[] roles = new[] { "Admin", "Profesora", "Patinadora", "Padre/Tutor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 2. Crear el usuario Administrador/Profesora inicial
            var adminEmail = "admin@escuela.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new Usuario
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Nombre = "Admin",
                    Apellidos = "Principal",
                    FechaNacimiento = DateTime.Parse("1990-01-01"),
                    Telefono = "123456789",
                    FotoUrl = "/images/usuarios/admin.png",
                };

                // Crea el usuario con una contraseña temporal
                var result = await userManager.CreateAsync(adminUser, "PasswordTemporal123*");

                if (result.Succeeded)
                {
                    // Asigna ambos roles
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    await userManager.AddToRoleAsync(adminUser, "Profesora");
                }
            }
        }
    }
}
