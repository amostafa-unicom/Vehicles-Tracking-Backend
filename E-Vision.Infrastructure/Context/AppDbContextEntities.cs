using E_Vision.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Vision.Infrastructure.Context
{
    public partial class AppDbContext
    {
        #region Main Entities
        public DbSet<Customer> Customer { get; set; } 
        public DbSet<Vehicle> Vehicle { get; set; } 
        public DbSet<VehicleTracking> VehicleTracking { get; set; } 
        #endregion
 

    }
}
