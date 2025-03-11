using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configure Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Enables Role Management
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddTransient<ApplicationDbSeeder>(); // Register the seeder as a transient service

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

// Migrate database, create roles, and seed data
using (var scope = app.Services.CreateScope())
{
    await CreateRoles(app.Services); // Your existing role creation

    var seeder = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
    await seeder.SeedAsync(); // Seed the database with sample data
}

app.Run();

async Task CreateRoles(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create an admin user if one does not exist
        string adminEmail = "admin@demo.com";
        string adminPassword = "Admin@123";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
            adminUser.EmailConfirmed = true;
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, "Admin");
            await userManager.AddClaimAsync(adminUser, new Claim("Department", "Owner"));
        }
    }
}