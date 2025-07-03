using Ambev.DeveloperEvaluation.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(si => si.Id);
        
        builder.Property(si => si.Id)
               .HasColumnType("uuid")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
               .IsRequired()
               .HasMaxLength(50);
        
        builder.Property(s => s.CreatedAt)
               .IsRequired();
        
        builder.Property(s => s.CustomerId)
               .IsRequired()
               .HasColumnType("uuid");
        
        builder.Property(s => s.CustomerName)
               .IsRequired()
               .HasMaxLength(100);
        
        builder.Property(s => s.BranchId)
               .IsRequired()
               .HasColumnType("uuid");
        
        builder.Property(s => s.BranchName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(s => s.Items)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(s => s.TotalAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();
        
        builder.Property(s => s.IsCancelled)
               .IsRequired();
    }
}
