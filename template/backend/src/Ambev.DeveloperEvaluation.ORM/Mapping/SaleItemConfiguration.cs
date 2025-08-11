namespace Ambev.DeveloperEvaluation.ORM.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        
        builder.Property(si => si.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(si => si.ProductExternalId)
            .HasColumnType("uuid")
            .IsRequired();
       
        builder.Property(si => si.Quantity)
            .IsRequired();
       
        builder.Property(si => si.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired(); 
              
        builder.Ignore(si => si.Total);
       
        builder.Property(si => si.Discount)
            .HasColumnType("decimal(5,4)")
            .IsRequired();
        
        builder.Property(si => si.IsCancelled)
            .IsRequired();
    }
}