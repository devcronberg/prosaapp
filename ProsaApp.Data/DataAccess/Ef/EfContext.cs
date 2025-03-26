using Microsoft.EntityFrameworkCore;
using ProsaApp.Domain.Types;

namespace ProsaApp.Data.DataAccess.Ef;
public class EfContext : DbContext
{
    private readonly string pathToDb;

    public DbSet<Customer> Customers { get; set; }

    public EfContext(string pathToDb)
    {
        this.pathToDb = pathToDb;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(pathToDb);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(e =>
        {
            e.ToTable("Customers");
        });
        base.OnModelCreating(modelBuilder);
    }
}