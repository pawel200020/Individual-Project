using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShopPortal.Entities;

namespace ShopPortal
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
        {
            
        }
        //fabryka jest konieczna ze względu na to że nie odpala się Program.cs, konfig jest pusty i migracja się wywala
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.Development.json")
                    .Build();
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersProducts>().HasKey(p => new {p.OrderId, p.ProductId});
            modelBuilder.Entity<ProductsCategories>().HasKey(p => new {p.CategoryId, p.ProductId});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }
        public DbSet<ProductsCategories> ProductsCategories { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
