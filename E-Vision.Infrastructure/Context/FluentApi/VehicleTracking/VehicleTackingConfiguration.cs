using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Vision.Infrastructure.Context.FluentApi.Vehicle
{
    public class VehicleTackingConfiguration : IEntityTypeConfiguration<Core.Entities.VehicleTracking>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.VehicleTracking> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasOne(e => e.Vehicle).WithMany(e => e.VehicleTracking).HasForeignKey(e => e.VehicleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
