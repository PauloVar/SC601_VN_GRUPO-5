using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto.BLL.Interfaces;
using Proyecto.BLL.Servicios;
using Proyecto.DAL.Context;
using Proyecto.DAL.Interfaces;
using Proyecto.DAL.Repositories;
using Proyecto.DAL.UnitsOfWork;
using Proyecto.ML.Entities;
using ProyectoPA_G5.Services;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// --- Contextos ---
builder.Services.AddDbContext<ProyectoTareasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ProyectoPADbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Model_first_connection")));

// --- Identity ---
builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ProyectoPADbContext>()
.AddDefaultTokenProviders();

// --- Repositorios y UnitOfWork ---
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ITareaService, TareaService>();
builder.Services.AddScoped<IPrioridadRepository, PrioridadRepository>();
builder.Services.AddScoped<IEstadoTareaRepository, EstadoTareaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddHostedService<TaskWorkerService>();

// Controllers & RazorPages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

var app = builder.Build();

// --- Middleware ---
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

// --- Rutas ---
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// --- Inicializar roles y admin ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();

//    string[] roles = { "Administrador", "Estudiante" };

    foreach (var roleName in roles)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole<int> { Name = roleName });
        }
    }

//    var adminEmail = "admin@domo.com";
//    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var user = new Usuario { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(user, "Admin123!");
        await userManager.AddToRoleAsync(user, "Administrador");
    }
}



app.Run();
