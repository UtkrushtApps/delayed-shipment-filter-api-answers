using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Carrier> Carriers => Set<Carrier>();
    public DbSet<Shipment> Shipments => Set<Shipment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrier>(entity =>
        {
            entity.ToTable("carriers");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.ToTable("shipments");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).HasColumnName("id");
            entity.Property(s => s.TrackingCode).HasColumnName("tracking_code");
            entity.Property(s => s.Destination).HasColumnName("destination");
            entity.Property(s => s.Status).HasColumnName("status");
            entity.Property(s => s.EstimatedDeliveryDate).HasColumnName("estimated_delivery_date");
            entity.Property(s => s.CarrierId).HasColumnName("carrier_id");
            entity.Property(s => s.CreatedAt).HasColumnName("created_at");
            entity.HasOne(s => s.Carrier)
                  .WithMany()
                  .HasForeignKey(s => s.CarrierId);
        });
    }
}
