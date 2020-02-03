using System.IO;
using iot_backend.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;

namespace iot_backend.Repository
{
    public class Repository : DbContext
    {
        
        public Repository()
        {
        }

        public Repository(DbContextOptions<Repository> options)
            : base(options)
        {
        }

        public virtual DbSet<IoTDataModel> TempDataModels { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("db_corelogin");
                //optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}