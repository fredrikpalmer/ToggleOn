using Microsoft.EntityFrameworkCore;
using ToggleOn.Domain;
using Env = ToggleOn.Domain.Environment;

namespace ToggleOn.EntityFrameworkCore.SqlServer;

internal class ToggleOnContext : DbContext
{
    public ToggleOnContext(DbContextOptions<ToggleOnContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Env> Environments { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<FeatureToggle> FeatureToggles { get; set; }
    public DbSet<FeatureGroup> FeatureGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>().ToTable(nameof(Project));

        modelBuilder.Entity<Project>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Project>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Project>()
            .HasAlternateKey(p => p.Name);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Environments)
            .WithOne(e => e.Project)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Features)
            .WithOne(f => f.Project)
            .HasForeignKey(f => f.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Env>().ToTable(nameof(Domain.Environment));

        modelBuilder.Entity<Env>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Env>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Env>()
            .HasAlternateKey(e => new { e.ProjectId, e.Name });

        modelBuilder.Entity<Env>()
            .HasMany(e => e.FeatureToggles)
            .WithOne(t => t.Environment)
            .HasForeignKey(t => t.EnvironmentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Feature>().ToTable(nameof(Feature));

        modelBuilder.Entity<Feature>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Feature>()
            .Property(f => f.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Feature>()
            .HasAlternateKey(f => new { f.ProjectId, f.Name });

        modelBuilder.Entity<Feature>()
            .HasMany(f => f.Toggles)
            .WithOne(t => t.Feature)
            .HasForeignKey(t => t.FeatureId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FeatureToggle>().ToTable(nameof(FeatureToggle));

        modelBuilder.Entity<FeatureToggle>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<FeatureToggle>()
            .Property(t => t.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<FeatureToggle>()
            .HasAlternateKey(t => new { t.FeatureId, t.EnvironmentId });

        modelBuilder.Entity<FeatureToggle>()
            .HasMany(t => t.Filters)
            .WithOne(r => r.FeatureToggle)
            .HasForeignKey(r => r.FeatureToggleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FeatureFilter>().ToTable(nameof(FeatureFilter));

        modelBuilder.Entity<FeatureFilter>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<FeatureFilter>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<FeatureFilter>()
            .HasMany(f => f.Parameters)
            .WithOne(p => p.Filter)
            .HasForeignKey(p => p.FeatureFilterId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FeatureFilterParameter>().ToTable(nameof(FeatureFilterParameter));

        modelBuilder.Entity<FeatureFilterParameter>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<FeatureFilterParameter>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<FeatureGroup>().ToTable(nameof(FeatureGroup));

        modelBuilder.Entity<FeatureGroup>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<FeatureGroup>()
            .HasAlternateKey(p => p.Name);
    }
}