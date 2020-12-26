using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Vision.Infrastructure.Context.FluentApi.Customer
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Core.Entities.Customer>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Customer> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.Address).HasMaxLength(100);
        }
    }
}
