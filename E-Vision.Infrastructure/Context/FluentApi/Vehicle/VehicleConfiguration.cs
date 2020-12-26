using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Vision.Infrastructure.Context.FluentApi.Vehicle
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Core.Entities.Vehicle>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Vehicle> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.VIN).HasMaxLength(50);
            builder.Property(e => e.Model).HasMaxLength(100);
            builder.HasOne(e => e.Customer).WithMany(e => e.Vehicle).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
