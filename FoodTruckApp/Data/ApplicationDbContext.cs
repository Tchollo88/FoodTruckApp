using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<FoodTruckApp.Models.Menu.Item> Items { get; set; } = default!;
        //public DbSet<FoodTruckApp.Models.Menu.Ingredient> Ingredients { get; set; } = default!;
        //public DbSet<FoodTruckApp.Models.Menu.Special> Soecials { get; set; } = default!;


    }
}
