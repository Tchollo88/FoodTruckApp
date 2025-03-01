using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodTruckApp.Models.Menu;
using FoodTruckApp.Models.Client;


namespace FoodTruckApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Special> Specials { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}

