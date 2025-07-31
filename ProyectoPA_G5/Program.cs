using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoPA_G5.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ProyectoPADbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ProyectoPADbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Cuenta}/{action=Login}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Administrador", "Estudiante" };

    foreach (var item in roles)
    {
        if (!await roleManager.RoleExistsAsync(item))
        {
            await roleManager.CreateAsync(new IdentityRole(item));
        }
    }

    var adminEmail = "admin@domo.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(user, "Admin123!");
        await userManager.AddToRoleAsync(user, "Administrador");
    }
}


app.Run();
