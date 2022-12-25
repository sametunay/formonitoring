using Microsoft.EntityFrameworkCore;

namespace API;

public class Context : DbContext
{
    public DbSet<SomeEntity> SomeEntities { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SomeEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Name).IsUnique(true);
        });
    }
}