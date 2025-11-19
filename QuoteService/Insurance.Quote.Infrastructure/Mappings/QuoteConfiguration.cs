using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quote.Domain.Entities;

namespace Insurance.Quote.Infrastructure.Mappings
{
    public class QuoteConfiguration : IEntityTypeConfiguration<QuoteEntity>
    {
        public void Configure(EntityTypeBuilder<QuoteEntity> builder)
        {
            builder.ToTable("Quotes");

            builder.HasKey(q => q.QuoteId);
            builder.Property(q => q.QuoteId).HasColumnName("quote_id");


            builder.Property(q => q.CustomerId).IsRequired();
            builder.Property(q => q.ProductId).IsRequired();
            builder.Property(q => q.InsuranceType).HasMaxLength(50);

            builder.Property(q => q.Status)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(q => q.EstimatedValue)
                   .HasColumnType("decimal(18, 2)");

            builder.Property(q => q.CreatedAt).IsRequired();

            builder.HasOne(q => q.Customer)
                   .WithMany() 
                   .HasForeignKey(q => q.CustomerId);

            builder.HasOne(q => q.Product)
                   .WithMany()
                   .HasForeignKey(q => q.ProductId);
        }
    }
}
