using Microsoft.EntityFrameworkCore;
using Minimal01.Domain.Entities;

namespace Minimal01.Infra.Data;

public class ApiDbContext : DbContext
{

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Vehicle> Cars { get; set; }

    public ApiDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Admin>()
            .HasData(
                new Admin()
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    Password = "senhaforte",
                    Profile = "Adm"
                }
        );
    }


}