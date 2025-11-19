using Insurance.Policy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insurance.Policy.Infrastructure.Persistence.Mappings
{
    public class PolicyConfiguration : IEntityTypeConfiguration<PolicyEntity>
    {
        public void Configure(EntityTypeBuilder<PolicyEntity> builder)
        {
            builder.ToTable("Policies");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("policy_id");

            builder.Property(p => p.QuoteId).IsRequired();

            builder.Property(p => p.ContractDate).IsRequired();

            builder.Property(p => p.PremiumValue)
                   .HasColumnType("decimal(18, 2)")
                   .IsRequired();

        }
    }
}