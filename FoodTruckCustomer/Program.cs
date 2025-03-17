using Microsoft.EntityFrameworkCore;
using Repository.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddTransient<ApplicationDbSeeder>(); // Register the seeder as a transient service

var app = builder.Build();

// ✅ Ensure static files (images, CSS, JS) are properly served
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

// Routing
app.UseRouting();
app.UseAuthorization();

// ✅ Remove `app.MapStaticAssets();` as it's unnecessary if `app.UseStaticFiles();` is used
// ✅ Ensure controller routes are correctly mapped
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
    await seeder.SeedAsync(); // Seed the database with sample data
}

app.Run();
