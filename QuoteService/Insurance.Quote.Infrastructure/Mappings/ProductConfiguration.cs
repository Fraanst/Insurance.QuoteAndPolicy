using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quote.Domain.Entities;

namespace Insurance.Quote.Infrastructure.Mappings
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        private readonly QuoteContext _context;

        public ProductConfiguration(QuoteContext context)
        {
            _context = context;
        }

        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId).HasColumnName("product_id");

            builder.Property(p => p.ProductType).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Value)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

        }
    }
}
