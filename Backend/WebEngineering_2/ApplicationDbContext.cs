namespace WebEngineering_2;
using Classes;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("reservations"); 
            
            entity.Property(e => e.id)
                .HasDefaultValueSql("uuid_generate_v4()");
            
            entity.Property(e => e.deleted_at)
                .HasColumnType("timestamp with time zone"); 
        });
    }
}
