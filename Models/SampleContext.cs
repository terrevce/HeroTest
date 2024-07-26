using Microsoft.EntityFrameworkCore;

namespace HeroTest.Models;

public partial class SampleContext : DbContext
{
    public SampleContext()
    {
    }

    public SampleContext(DbContextOptions<SampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; } = null!;
    public virtual DbSet<Hero> Heroes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<Hero>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(50);

            entity.Property(e => e.BrandId).HasDefaultValueSql("((1))");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Brand)
                .WithMany(p => p.Heroes)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Heroes__BrandId__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public Brand? GetBrandByName(string name)
    {
        return this.Brands.Where(x=>x.Name == name).FirstOrDefault();
    }
}
