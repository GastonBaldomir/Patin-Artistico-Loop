using Loop_Patin.Data;
using Loop_Patin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------- SERVICIOS ----------------

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("LoopConnection");
builder.Services.AddDbContext<LoopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<LoopDbContext>()
.AddDefaultTokenProviders();

// ---------------- BUILD ----------------

var app = builder.Build();

// ---------------- SEEDER ----------------

await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbSeeder.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al sembrar la base de datos.");
    }
}

// ---------------- MIDDLEWARE ----------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
