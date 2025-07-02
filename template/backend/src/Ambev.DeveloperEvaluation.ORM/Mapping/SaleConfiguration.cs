using Ambev.DeveloperEvaluation.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.Infra.Data.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(s => s.IsCancelled)
               .IsRequired();

        builder.Property(s => s.CreatedAt)
               .IsRequired();

        builder.Property(s => s.TotalAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.HasOne(s => s.Customer)
               .WithMany()
               .HasForeignKey(s => s.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Branch)
               .WithMany()
               .HasForeignKey(s => s.BranchId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Items)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
