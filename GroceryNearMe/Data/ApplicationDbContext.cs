using GroceryNearMe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroceryNearMe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GroceryNearMe.Models.Address>? Addresses { get; set; }
        public DbSet<GroceryNearMe.Models.Category>? Categories { get; set; }

        public DbSet<GroceryNearMe.Models.Company>? Companies { get; set; }

        public DbSet<GroceryNearMe.Models.Store>? Stores  { get; set; }
            
        public DbSet<GroceryNearMe.Models.Review>? Reviews  { get; set; }

        public DbSet<GroceryNearMe.Models.Product>? Products { get; set; }

        public DbSet<GroceryNearMe.Models.CartItem>? CartItems { get; set; }




    }
}