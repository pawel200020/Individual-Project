using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineShop.Entities;
using Microsoft.Extensions.Configuration;

namespace OnlineShop
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
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
        public DbSet<Category> Categories { get; set; }
    }
}
