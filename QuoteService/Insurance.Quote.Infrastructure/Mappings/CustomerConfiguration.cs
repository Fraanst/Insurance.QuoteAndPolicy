using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quote.Domain.Entities;

namespace Insurance.Quote.Infrastructure.Mappings
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.CustomerId);
            builder.Property(c => c.CustomerId).HasColumnName("customer_id");

            builder.Property(c => c.CustomerName).HasMaxLength(250).IsRequired();
            builder.HasIndex(c => c.DocumentNumber).IsUnique();
            builder.Property(c => c.BirthDate).IsRequired();

            builder.HasMany(c => c.Quotes) 
                   .WithOne(q => q.Customer) 
                   .HasForeignKey(q => q.CustomerId) 
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
