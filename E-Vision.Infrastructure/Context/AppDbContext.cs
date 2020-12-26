using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Vision.Infrastructure.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {


        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=E_Vision_Task;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Fluent APIs
            base.OnModelCreating(modelBuilder); 
        }


    }
}
