using Microsoft.EntityFrameworkCore;
using PopulationCount.Entities;

namespace PopulationCount;

public partial class CitystatecountryContext : DbContext
{
    public CitystatecountryContext()
    {
    }

    public CitystatecountryContext(DbContextOptions<CitystatecountryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=citystatecountry.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.HasKey(e => e.CityId);
            entity.Property(e => e.CityId)
                .ValueGeneratedNever()
                .HasColumnType("INT");

            entity.Property(e => e.CityName).HasColumnType("varchar(2000)");
            entity.Property(e => e.Population).HasColumnType("INT");
            entity.Property(e => e.StateId).HasColumnType("INT");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.HasKey(e => e.StateId);

            entity.Property(e => e.StateId)
                .ValueGeneratedNever()
                .HasColumnType("INT");

            entity.Property(e => e.CountryId).HasColumnType("INT");
            entity.Property(e => e.StateName).HasColumnType("varchar(2000)");

            entity.HasOne(d => d.Country)
                .WithMany(d => d.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(d => d.Cities)
                .WithOne(d => d.State)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.HasKey(e => e.CountryId);

            entity.Property(e => e.CountryId).HasColumnType("INT");
            entity.Property(e => e.CountryName).HasColumnType("varchar(2000)");
            entity.HasMany(d => d.States)
                .WithOne(d => d.Country)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
