using System.Collections.Generic;
using AbbaraShowroom.Models;
using Microsoft.EntityFrameworkCore;

namespace AbbaraShowroom.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }


    }
}
